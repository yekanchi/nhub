<Query Kind="Statements" />

object obj = "test";

if (obj is string { Length:4 })
	Console.WriteLine ("string with length of 4");