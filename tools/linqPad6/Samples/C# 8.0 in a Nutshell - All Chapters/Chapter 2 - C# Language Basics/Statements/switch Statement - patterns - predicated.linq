<Query Kind="Statements" />

object x = true;

switch (x)
{
	case bool b when b == true:     // Fires only when b is true
		Console.WriteLine ("True!");
		break;
	case bool b:
		Console.WriteLine ("False!");
		break;
}