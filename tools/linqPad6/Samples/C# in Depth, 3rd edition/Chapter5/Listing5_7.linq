<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

Predicate<int> isEven = delegate(int x)
    { return x % 2 == 0; };

Console.WriteLine(isEven(1));
Console.WriteLine(isEven(4));