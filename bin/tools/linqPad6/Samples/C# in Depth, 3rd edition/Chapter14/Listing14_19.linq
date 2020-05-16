<Query Kind="Program" />

void Main()
{
    Base receiver = new Derived();
    dynamic d = "text";
    receiver.Execute(d);    
}

class Base
{
    public void Execute(object x)
    {
        Console.WriteLine("object");
    }
}

class Derived : Base
{
    public void Execute(string x)
    {
        Console.WriteLine("string");
    }
}
