<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    PrintType(1, new object());
}

static void PrintType<T>(T first, T second)
{
    Console.WriteLine(typeof(T));
}
