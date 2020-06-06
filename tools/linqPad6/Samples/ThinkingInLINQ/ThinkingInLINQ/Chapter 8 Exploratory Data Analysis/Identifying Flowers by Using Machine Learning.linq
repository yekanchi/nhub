<Query Kind="Program">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

public static class EnumEx
{
    public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> list, int length) 
    {
        return list.OrderBy<T,Guid>(t => Guid.NewGuid()).Take(length);
    }
}
void Main()
{	
	//Nearest Neighbor
	var trainingSet = File.ReadAllText(@"C:\iris.csv")
					      .Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries)
	                      .Select ( f => f.Split(','))
	                      .Skip(1)
						  .Select (f =>
									new
									{
										SepalLength = Convert.ToDouble( f[0]),
										SepalWidth = Convert.ToDouble(f[1]),
										PetalLength = Convert.ToDouble(f[2]),
										PetalWidth = Convert.ToDouble(f[3]),
										Name = f[4]
									})
						.RandomSubset(100);
	
	trainingSet.Dump("Training set");
	//Test data
	double sepalLength = 5.5;
	double sepalWidth = 2.6;
	double petalLength = 4;
	double petalWidth = 1.2;
	int k = 5;
	
	//Specific Euclidean distance function
	Func<double,double,double,double,double,double,double,double,double> Distance =
					(sl1,sl2,sw1,sw2,pl1,pl2,pw1,pw2) => Math.Sqrt(Math.Pow(sl1-sl2,2)
														+ Math.Pow(sw1-sw2,2)
														+ Math.Pow(pl1-pl2,2)
														+ Math.Pow(pw1-pw2,2));
	//Figure out what flower it is.
	trainingSet
			.Select (s => 
						new
						{
							Name = s.Name,
							DistanceFromTestData =
							Distance(sepalLength,s.SepalLength,sepalWidth,s.SepalWidth,
									petalLength, s.PetalLength, petalWidth, s.PetalWidth)
						})
			.OrderBy (s => s.DistanceFromTestData )
			//Take the first "k" elements
			.Take(k)
			//Create a lookup with the "Name"
			.ToLookup (s => s.Name)
			//Sort the elements as per the descending order of number of elements in that class
			.OrderByDescending (s => s.Count())
			//Pick the first one--with the highest count
			.First ()
			//Pick its class
			.Key
			.Dump("I think the flower is");
}
