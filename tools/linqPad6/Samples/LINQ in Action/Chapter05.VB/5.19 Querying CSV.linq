<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Dim csv = _
	"#Books (format: ISBN, Title, Authors, Publisher, Date, Price)" + Environment.NewLine + _
	"0735621632,CLR via C#,Jeffrey Richter,Microsoft Press,02-22-2006,59.99"  + Environment.NewLine + _
	"0321127420,Patterns Of Enterprise Application Architecture,Martin Fowler,Addison-Wesley, 11-05-2002,54.99" + Environment.NewLine + _
	"0321200683,Enterprise Integration Patterns,Gregor Hohpe,Addison-Wesley,10-10-2003,54.99" + Environment.NewLine + _
	"0321125215,Domain-Driven Design,Eric Evans,Addison-Wesley Professional,08-22-2003,54.99" + Environment.NewLine + _
	"1932394613,Ajax In Action,Dave Crane;Eric Pascarello;Darren James,Manning Publications,10-01-2005,44.95"

Dim books = _
  From line In csv.Split(Environment.NewLine) _
  Where Not line.StartsWith("#") _
	Let parts = line.Split(",") _
	Select New With {.Isbn = parts(0), .Title = parts(1), .Publisher = parts(3)}

books.Dump()