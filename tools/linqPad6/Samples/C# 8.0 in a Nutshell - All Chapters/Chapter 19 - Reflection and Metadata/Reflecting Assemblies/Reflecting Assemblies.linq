<Query Kind="Program" />

void Main()
{
	Assembly currentAssem = Assembly.GetExecutingAssembly();
	
	var t = currentAssem.GetType ("Demos.TestProgram");
	t.Dump();
	
	var allTypes = currentAssem.GetTypes().Dump();
}

namespace Demos
{
	class TestProgram
	{    
	}
	
	class SomeOtherType
	{
	}
}