<Query Kind="Statements">
  <Namespace>System.Collections.Immutable</Namespace>
</Query>

var oldList = ImmutableList.Create<int> (1, 2, 3);

ImmutableList<int> newList = oldList.Add (4);

Console.WriteLine (oldList.Count);     // 3  (unaltered)
Console.WriteLine (newList.Count);     // 4

var anotherList = oldList.AddRange (new[] { 4, 5, 6 });
anotherList.Dump();