<Query Kind="Statements" />

Type base1 = typeof (System.String).BaseType;
Type base2 = typeof (System.IO.FileStream).BaseType;

Console.WriteLine (base1.Name);     // Object
Console.WriteLine (base2.Name);     // Stream

Console.WriteLine ();

foreach (Type iType in typeof (Guid).GetInterfaces())
	Console.WriteLine (iType.Name);