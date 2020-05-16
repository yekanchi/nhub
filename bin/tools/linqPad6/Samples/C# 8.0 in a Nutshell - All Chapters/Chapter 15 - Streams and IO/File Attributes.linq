<Query Kind="Statements" />

string filePath = @"test.txt";
File.WriteAllText(filePath, "The quick brown fox.");

FileAttributes fa = File.GetAttributes (filePath);
if ((fa & FileAttributes.ReadOnly) != 0)
{
	// Use the exclusive-or operator (^) to toggle the ReadOnly flag
	fa ^= FileAttributes.ReadOnly;
	File.SetAttributes (filePath, fa);
}

Console.WriteLine (new FileInfo ("test.txt").IsReadOnly);
new FileInfo ("test.txt").IsReadOnly = true;
Console.WriteLine (new FileInfo ("test.txt").IsReadOnly);
new FileInfo ("test.txt").IsReadOnly = false;
Console.WriteLine (new FileInfo ("test.txt").IsReadOnly);


// Now we can delete the file, for instance:
File.Delete (filePath);