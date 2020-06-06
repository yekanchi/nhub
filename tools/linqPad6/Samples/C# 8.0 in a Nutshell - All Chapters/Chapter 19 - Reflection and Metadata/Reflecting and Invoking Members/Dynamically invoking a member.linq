<Query Kind="Statements" />

object s = "Hello";
PropertyInfo prop = s.GetType().GetProperty ("Length");
int length = (int)prop.GetValue (s, null);               // 5

length.Dump();