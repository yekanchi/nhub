<Query Kind="Program" />

static (string,int) GetPerson() => ("Bob", 23);

static void Main()
{
	(string, int) person = GetPerson();   // Could use 'var' here if we want
	Console.WriteLine (person.Item1);    // Bob
	Console.WriteLine (person.Item2);    // 23
}