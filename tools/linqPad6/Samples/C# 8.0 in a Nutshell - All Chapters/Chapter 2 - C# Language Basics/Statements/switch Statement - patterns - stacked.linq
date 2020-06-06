<Query Kind="Statements" />

object x = 3000m;

switch (x)
{
	case float f when f > 1000:
	case double d when d > 1000:
	case decimal m when m > 1000:
		Console.WriteLine ("We can refer to x here but not f or d or m");
		break;
}