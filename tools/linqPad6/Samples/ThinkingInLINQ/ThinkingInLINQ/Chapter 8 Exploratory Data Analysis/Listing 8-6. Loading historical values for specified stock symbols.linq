<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

/*
	To run this code you shall need to reference "System.Net" 
	because it uses WebClient. 
	
	For determining the URL 
	follow the following steps
	
	1. Go to http://finance.yahoo.com/q/hp?s=MSFT
	2. Once the page completely loads scroll to the bottom of the page
	3. You shall see there is a link for "Download to spreadsheet"
	4. Right click on that link and select "Copy link location"
	5. Paste the link location in your code 
	   it will look like this 
	   http://real-chart.finance.yahoo.com/table.csv?s=MSFT&d=11&e=9&f=2014&g=d&a=0&b=2&c=1962&ignore=.csv
	   
	6. Note that the symbol is embedded as part of the link "s=MSFT" 
	7. Change that to {0} as {0} will be the place holder for the symbol at runtime. 
	http://real-chart.finance.yahoo.com/table.csv?s={0}&d=11&e=9&f=2014&g=d&a=0&b=2&c=1962&ignore=.csv
	
	8. Use this URL in the download file call in the code below.
*/

string[] symbols = {"AAPL","GOOG","MSFT","HP","INTL"};
WebClient wc = new WebClient();
//This structure will hold the stock values
List<Tuple<string,DateTime,double,double,double,double,double,Tuple<double>>> mapping 
     = new List<Tuple<string,DateTime,double,double,double,double,double,Tuple<double>>>();
foreach (var symbol in symbols)
{
	File.Delete("temp.csv");
	
	wc.DownloadFile(
		String.Format(
		//Make sure the following URL string appears in a single line. Otherwise, the program won't work
		//http://real-chart.finance.yahoo.com/table.csv?s=GE&d=11&e=9&f=2014&g=d&a=0&b=2&c=1962&ignore=.csv
		@"http://real-chart.finance.yahoo.com/table.csv?s={0}&d=11&e=9&f=2014&g=d&a=0&b=2&c=1962&ignore=.csv",symbol),"temp.csv");
	
	mapping.AddRange(File.ReadAllLines(@"temp.csv")
			.Skip(1)//Skip the header
			.Select( l =>
			{
				var toks = l.Split(',');
				return new Tuple<string,DateTime,double,double,double,double,double,Tuple<double>>
				(
						symbol,
						DateTime.Parse(toks[0]),
						Convert.ToDouble(toks[1]),
						Convert.ToDouble(toks[2]),
						Convert.ToDouble(toks[3]),
						Convert.ToDouble(toks[4]),
						Convert.ToDouble(toks[5]),
						//The last element must be a Tuple again.
						new Tuple<double>(Convert.ToDouble(toks[6]))
				 );
			 }));
}
var stocks = mapping.Select (m =>
									new
									{
										Symbol = m.Item1,
										Date = m.Item2,
										Open = m.Item3,
										High = m.Item4,
										Low = m.Item5,
										Close = m.Item6,
										Volume = m.Item7,
										AdjClose = m.Rest.Item1
									})
						.ToLookup (m => m.Symbol)
						.SelectMany (m => m.Take(7))
						.OrderBy (m => m.Date);

stocks.Dump("Stock values for last week");
