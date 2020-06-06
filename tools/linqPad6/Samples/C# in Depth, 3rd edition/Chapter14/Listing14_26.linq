<Query Kind="Statements">
  <Namespace>System.Dynamic</Namespace>
</Query>

dynamic expando = new ExpandoObject();
expando.AddOne = (Func<int, int>)(x => x + 1);
Console.Write(expando.AddOne(10));
