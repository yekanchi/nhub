<Query Kind="Program">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

void Main()
{
  XElement name = new XElement("name", GetUsersName());
  Console.WriteLine(name);
}

private string GetUsersName() { return "George"; }