<Query Kind="Statements" />

string s1 = null;
s1 ??= "something";
Console.WriteLine (s1);  // something

s1 ??= "everything";
Console.WriteLine (s1);  // something
