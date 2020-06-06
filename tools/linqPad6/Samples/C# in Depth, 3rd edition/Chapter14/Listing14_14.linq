<Query Kind="Program" />

void Main()
{
    PrintCount(new BitArray(10));
    PrintCount(new HashSet<int> { 3, 5 });
    PrintCount(new List<int> { 1, 2, 3 });    
}

static void PrintCount(IEnumerable collection)
{
    dynamic d = collection;
    int count = d.Count;
    Console.WriteLine(count);
}
