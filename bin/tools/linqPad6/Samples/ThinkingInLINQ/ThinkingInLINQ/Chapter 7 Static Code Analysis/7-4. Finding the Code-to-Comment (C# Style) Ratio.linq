<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

string code = @"//This is a test
int x = 10;//set x to 10
//increase x by one
x++;
var rad = Radius(x);//Find radius";
var lookup = code.Split(new string[]{Environment.NewLine,";"},StringSplitOptions.RemoveEmptyEntries)
				 .Select (line => line.Trim())
				 .Select (line => new 
				 			{
								Line = line,
								IsComment = line.StartsWith("//")
							})
				.ToLookup (line => line.IsComment);
lookup
	.Select (entry =>
		new
		{
			Component = entry.Key==true?"Comment":"Code",
			Percentage = 100*Math.Round((double)entry.Count()/
						(double)lookup.SelectMany (l => l).Count(),2)
		})
.Dump("Code to Comment Ratio");
