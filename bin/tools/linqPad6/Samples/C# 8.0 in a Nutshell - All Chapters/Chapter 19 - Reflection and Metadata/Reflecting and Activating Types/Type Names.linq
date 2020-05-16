<Query Kind="Program" />

void Main()
{
	{
		Type t = typeof (System.Text.StringBuilder);

		Console.WriteLine (t.Namespace);      // System.Text
		Console.WriteLine (t.Name);           // StringBuilder
		Console.WriteLine (t.FullName);       // System.Text.StringBuilder
	}

	// Nested type names
	{
		Type t = typeof (System.Environment.SpecialFolder);

		Console.WriteLine (t.Namespace);      // System
		Console.WriteLine (t.Name);           // SpecialFolder
		Console.WriteLine (t.FullName);       // System.Environment+SpecialFolder
	}

	// Generic type names
	{
		Type t = typeof (Dictionary<,>);   // Unbound
		Console.WriteLine (t.Name);        // Dictionary'2
		Console.WriteLine (t.FullName);    // System.Collections.Generic.Dictionary'2
		Console.WriteLine (typeof (Dictionary<int, string>).FullName);
	}

	// Array and pointer type names
	{
		Console.WriteLine (typeof (int[]).Name);        // Int32[]
		Console.WriteLine (typeof (int [,]).Name);      // Int32[,]
		Console.WriteLine (typeof (int [,]).FullName);  // System.Int32[,]
	}

	// Pointer types
	Console.WriteLine (typeof (byte*).Name);     // Byte*

	// ref and out parameter type names
	int x = 3;
	RefMethod (ref x);
}

public void RefMethod (ref int p)
{
	Type t = MethodInfo.GetCurrentMethod().GetParameters() [0].ParameterType;
	Console.WriteLine (t.Name);    // Int32&
}