<Query Kind="Statements" />

Type closed = typeof (List<int>);
List<int> list = (List<int>) Activator.CreateInstance (closed);  // OK

Type unbound   = typeof (List<>).Dump ("unbound", 1);
try
{
	object anError = Activator.CreateInstance (unbound);    // Runtime error
}
catch (Exception ex)
{
	ex.Dump ("You cannot instantiate an unbound type");
}

// The MakeGenericType method converts an unbound into a closed generic type.
closed = unbound.MakeGenericType (typeof (int)).Dump ("closed", 1);

//The GetGenericTypeDefinition method does the opposite:
Type unbound2 = closed.GetGenericTypeDefinition();  // unbound == unbound2

// The IsGenericType property returns true if a Type is generic, and the
// IsGenericTypeDefinition property returns true if the generic type is unbound.
// The following tests whether a type is a nullable value type:
Type nullable = typeof (bool?);
Console.WriteLine (
	nullable.IsGenericType &&
	nullable.GetGenericTypeDefinition() == typeof (Nullable<>));   // True

//GetGenericArguments returns the type arguments for closed generic types:
Console.WriteLine (closed.GetGenericArguments() [0]);     // System.Int32
Console.WriteLine (nullable.GetGenericArguments() [0]);   // System.Boolean

// For unbound generic types, GetGenericArguments returns pseudotypes that
// represent the placeholder types specified in the generic type definition:
Console.WriteLine (unbound.GetGenericArguments() [0]);      // T