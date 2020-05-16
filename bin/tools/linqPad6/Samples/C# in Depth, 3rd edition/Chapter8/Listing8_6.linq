<Query Kind="Statements">
  <Reference Relative="Chapter08.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter8\Chapter08.exe</Reference>
  <Namespace>Chapter08</Namespace>
</Query>

List<Person> family = new List<Person>
{
    new Person { Name="Holly", Age=37 },
    new Person { Name="Jon", Age=36 },
    new Person { Name="Tom", Age=9 },
    new Person { Name="Robin", Age=6 },
    new Person { Name="William", Age=6 }
};

var converted = family.ConvertAll(delegate(Person person)
    { return new { person.Name, IsAdult = (person.Age >= 18) }; }
);

foreach (var person in converted)
{
    Console.WriteLine("{0} is an adult? {1}",
                      person.Name, person.IsAdult);
}