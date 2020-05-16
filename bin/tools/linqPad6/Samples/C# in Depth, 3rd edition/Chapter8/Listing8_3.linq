<Query Kind="Statements">
  <Reference Relative="Chapter08.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter8\Chapter08.exe</Reference>
  <Namespace>Chapter08</Namespace>
</Query>

Person tom = new Person
{
    Name = "Tom",
    Age = 9,
    Home = { Town = "Reading", Country = "UK" },
    Friends =
    {
        new Person { Name = "Alberto" },
        new Person("Max"),
        new Person { Name = "Zak", Age = 7 },
        new Person("Ben"),
        new Person("Alice")
        {
            Age = 9,
            Home = { Town = "Twyford", Country="UK" }
        }
    }
};