<Query Kind="Program" />

void Main()
{
    dynamic text = "text";
    Execute(text);
    dynamic number = 10;
    Execute(number);    
}

static void Execute(string x)
{
   Console.WriteLine("String overload");
}

static void Execute(dynamic x)
{
   Console.WriteLine("Dynamic overload");
}

