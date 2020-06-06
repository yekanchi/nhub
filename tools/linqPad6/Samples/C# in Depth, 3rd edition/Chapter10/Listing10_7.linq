<Query Kind="Statements" />

var collection = Enumerable.Range(0, 10)
               .Reverse();

foreach (var element in collection)
{
    Console.WriteLine(element);
}

