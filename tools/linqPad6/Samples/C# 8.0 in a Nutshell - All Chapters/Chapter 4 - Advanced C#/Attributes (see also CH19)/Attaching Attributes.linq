<Query Kind="Program" />

void Main()
{
	new Foo();   // Generates a warning because Foo is obsolete
}

[Obsolete]
public class Foo 
{  
}