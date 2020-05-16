<Query Kind="Statements" />

Func<string, int> returnLength;
returnLength = text => text.Length;

Console.WriteLine(returnLength("Hello"));
