<Query Kind="Program" />

void Main()
{
	var str = "some";
	var ms = new MemoryStream();
	
	byte[] bits = Encoding.ASCII.GetBytes(str);
	
	bits.Dump();
}

// Define other methods, classes and namespaces here
