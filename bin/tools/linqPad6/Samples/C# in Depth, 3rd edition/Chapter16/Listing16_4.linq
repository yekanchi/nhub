<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

void Main()
{
    dynamic x = new TypeUsedDynamically();
    x.ShowCaller();
}

class TypeUsedDynamically
{
    internal void ShowCaller([CallerMemberName] string caller = "Unknown")
    {
        Console.WriteLine("Called by: {0}", caller);
    }
}
