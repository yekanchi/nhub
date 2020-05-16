<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
</Query>

ArrayList list = new ArrayList { "First", "Second", "Third"};
var strings = from string entry in list
              select entry.Substring(0, 3);

foreach (string start in strings)
{
    Console.WriteLine(start);
}
