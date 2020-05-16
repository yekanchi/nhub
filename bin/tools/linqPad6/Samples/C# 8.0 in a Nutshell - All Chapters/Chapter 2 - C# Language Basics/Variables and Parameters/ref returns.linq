<Query Kind="Program" />

// You can return a ref local from a method. This is called a ref return:
static string X = "Old Value";

static ref string GetX() => ref X;    // This method returns a ref

static void Main()
{
	ref string xRef = ref GetX();       // Assign result to a ref local
	xRef = "New Value";
	Console.WriteLine (X);              // New Value
}