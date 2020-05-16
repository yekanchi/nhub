<Query Kind="Program" />

void Main()
{
    Type type = GetType(); // Was typeof(Snippet) for Snippy
    MethodInfo definition = type.GetMethod("PrintTypeParameter");
    MethodInfo constructed;
    constructed = definition.MakeGenericMethod(typeof(string));
    constructed.Invoke(null, null);    
}

public static void PrintTypeParameter<T>()
{
    Console.WriteLine (typeof(T));
}
