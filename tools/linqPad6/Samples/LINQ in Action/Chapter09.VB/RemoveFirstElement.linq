<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"

Dim books As XElement = XElement.Load(Path & "existingBooks.xml.sdf")
books.Element("book").Remove()  'remove the first book
Console.WriteLine(books)