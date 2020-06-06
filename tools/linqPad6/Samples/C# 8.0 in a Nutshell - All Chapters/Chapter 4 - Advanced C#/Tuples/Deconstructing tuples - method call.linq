<Query Kind="Program" />

static (string, int, char) GetBob() => ( "Bob", 23, 'M');

static void Main()
{
	var (name, age, sex) = GetBob();
	Console.WriteLine (name);        // Bob
	Console.WriteLine (age);         // 23
	Console.WriteLine (sex);         // M
}