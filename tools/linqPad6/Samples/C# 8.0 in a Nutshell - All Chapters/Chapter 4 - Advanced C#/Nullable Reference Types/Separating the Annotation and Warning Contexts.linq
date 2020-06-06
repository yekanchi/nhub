<Query Kind="Program" />

#nullable enable annotations   // Enable just the nullable annotation context

// Because we've enabled the annotation context, s1 is non-nullable, and s2 is nullable:
public void Foo (string s1, string? s2)
{
	// Our use of s2.Length doesn't generate a warning, however,
	// because we've enabled just the annotation context:
	Console.Write (s2.Length);
}

void Main() 
{
	// Now let's enable the warning context, too  
	#nullable enable warnings
	
	// Notice that this now generates a warning:  
	Foo (null, null);
}