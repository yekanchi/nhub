<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

var csv =
@"#Books (format: ISBN, Title, Authors, Publisher, Date, Price)
0735621632,CLR via C#,Jeffrey Richter,Microsoft Press,02-22-2006,59.99
0321127420,Patterns Of Enterprise Application Architecture,Martin Fowler,Addison-Wesley, 11-05-2002,54.99
0321200683,Enterprise Integration Patterns,Gregor Hohpe,Addison-Wesley,10-10-2003,54.99
0321125215,Domain-Driven Design,Eric Evans,Addison-Wesley Professional,08-22-2003,54.99
1932394613,Ajax In Action,Dave Crane;Eric Pascarello;Darren James,Manning Publications,10-01-2005,44.95";

var books =
  from line in csv.Split('\n') //File.ReadAllLines("books.csv")
  where !line.StartsWith("#")
  let parts = line.Split(',')
  select new Book {
    Isbn = parts[0],
    Title = parts[1],
    Publisher = new Publisher { Name = parts[3] },
    Authors =
	  from authorFullName in parts[2].Split(';')
      let authorNameParts = authorFullName.Split(' ')
      select new Author {
        FirstName = authorNameParts[0],
        LastName = authorNameParts[1]
      }
    };

books.Dump();