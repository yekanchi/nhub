<Query Kind="Statements" />

// The MemberInfo objects returned from unbound and closed generic types are always distinct
// — even for members whose signatures don’t feature generic type parameters:

PropertyInfo unbound = typeof (List<>).GetProperty ("Count");
PropertyInfo closed = typeof (List<int>).GetProperty ("Count");

Console.WriteLine (unbound);   // Int32 Count
Console.WriteLine (closed);    // Int32 Count

Console.WriteLine (unbound == closed);   // False

Console.WriteLine (unbound.DeclaringType.IsGenericTypeDefinition); // True
Console.WriteLine (closed.DeclaringType.IsGenericTypeDefinition); // False
