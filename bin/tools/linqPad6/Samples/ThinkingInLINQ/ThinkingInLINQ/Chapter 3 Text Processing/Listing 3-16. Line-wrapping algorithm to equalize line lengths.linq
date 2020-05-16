<Query Kind="Statements">
  
</Query>

string text =
@"Almost any text editor provides a fill
operation. The fill operation transforms raggedy-looking text
with lines of
different lengths into nicely formatted text with lines
nearly the same length.";
text.Dump("Before");
var words = text.Split(new char[]{' ','\r','\n'},StringSplitOptions.RemoveEmptyEntries)
				.Where (t => t.Trim().Length!=0);
var lines = text.Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries);
int max = lines
			.Select(l => l.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).Count ())
			.OrderByDescending (l => l)
			.First();
max = max + max / 2;//Maximum width is 1.5 times that of the current maximum width
Enumerable.Range(0,words.Count ()/max + 1)//decide how many lines need to be there.
		  .Select(k => words.Skip(k*max).Take(max).Aggregate ((u,v) => u + " " + v))
          .Aggregate ((m,n) => m + Environment.NewLine + n)//provide line breaks
           .Dump("After");
