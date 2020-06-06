<Query Kind="Statements">
  
</Query>

int[] array = { 3, 4, 3, 2, 3, -1, 3, 3};
array.ToLookup (a => a).First (a => a.Count() > array.Length/2).Key.Dump("Dominator");