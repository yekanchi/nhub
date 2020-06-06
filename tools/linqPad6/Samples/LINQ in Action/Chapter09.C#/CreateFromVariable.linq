<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string usersName = "Fred";
XElement name = new XElement("name", usersName);
Console.WriteLine(name);