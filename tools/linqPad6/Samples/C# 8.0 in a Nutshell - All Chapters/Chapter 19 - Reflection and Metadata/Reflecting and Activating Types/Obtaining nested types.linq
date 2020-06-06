<Query Kind="Statements" />

foreach (Type t in typeof (System.Environment).GetNestedTypes())
	Console.WriteLine (t.FullName);

// The CLR treats a nested type as having special “nested” accessibility levels:
Type sf = typeof (System.Environment.SpecialFolder);
Console.WriteLine (sf.IsPublic);                      // False
Console.WriteLine (sf.IsNestedPublic);                // True