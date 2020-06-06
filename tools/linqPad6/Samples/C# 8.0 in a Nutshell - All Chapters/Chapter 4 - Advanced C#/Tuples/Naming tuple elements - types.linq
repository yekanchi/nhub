<Query Kind="Program" />

static (string Name, int Age) GetPerson() => ("Bob", 23);

static void Main()
{
	var person = GetPerson();
	Console.WriteLine (person.Name);    // Bob
	Console.WriteLine (person.Age);     // 23
}