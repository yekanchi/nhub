<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

void Main()
{
	var csv =
@"#Books (format: ISBN, Title, Authors, Publisher, Date, Price)
0735621632,CLR via C#,Jeffrey Richter,Microsoft Press,02-22-2006,59.99
0321127420,Patterns Of Enterprise Application Architecture,Martin Fowler,Addison-Wesley, 11-05-2002,54.99
0321200683,Enterprise Integration Patterns,Gregor Hohpe,Addison-Wesley,10-10-2003,54.99
0321125215,Domain-Driven Design,Eric Evans,Addison-Wesley Professional,08-22-2003,54.99
1932394613,Ajax In Action,Dave Crane;Eric Pascarello;Darren James,Manning Publications,10-01-2005,44.95";
	
  using (var reader = new StringReader(csv) /*new StreamReader("books.csv")*/)
  {
    var books =
      from line in reader.Lines().Reverse() // Reverse breaks streaming because it processes all lines at once
      where !line.StartsWith("#")
      let parts = line.Split(',')
      select new { Isbn=parts[0], Title=parts[1], Publisher=parts[3] };
 
    books.Dump();

    // Warning, the reader should not be disposed while we are likely to enumerate the query!
    // Don't forget that deferred execution happens here
  }
}

}// Temporary hack to enable extension methods

/// <summary>
/// Operators for LINQ to Text Files
/// </summary>
public static class Extensions
{
  public static IEnumerable<String> Lines(this TextReader source)
  {
    String line;

    if (source == null)
	  throw new ArgumentNullException("source");

    while ((line = source.ReadLine()) != null)
      yield return line;
  }