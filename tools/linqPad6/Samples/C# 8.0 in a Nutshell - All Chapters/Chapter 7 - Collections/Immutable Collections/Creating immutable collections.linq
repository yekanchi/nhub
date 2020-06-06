<Query Kind="Statements">
  <Namespace>System.Collections.Immutable</Namespace>
</Query>

ImmutableArray<int> array = ImmutableArray.Create<int> (1, 2, 3);

var list = new[] { 1, 2, 3 }.ToImmutableList();

array.Dump();
list.Dump();