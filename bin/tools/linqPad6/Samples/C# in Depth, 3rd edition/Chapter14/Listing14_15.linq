<Query Kind="Program" />

void Main()
{
    PrintCount(new BitArray(5));
    PrintCount(new HashSet<int> { 1, 2 });
    PrintCount("ABC");
    PrintCount("ABCDEF".Where(c => c > 'B'));    
}

private static int CountImpl<T>(ICollection<T> collection)
{
    return collection.Count;
}

private static int CountImpl(ICollection collection)
{
    return collection.Count;
}

private static int CountImpl(string text)
{
    return text.Length;
}

private static int CountImpl(IEnumerable collection)
{
    int count = 0;
    foreach (object item in collection)
    {
        count++;
    }
    return count;
}

public static void PrintCount(IEnumerable collection)
{
    dynamic d = collection;
    int count = CountImpl(d);
    Console.WriteLine(count);
}

