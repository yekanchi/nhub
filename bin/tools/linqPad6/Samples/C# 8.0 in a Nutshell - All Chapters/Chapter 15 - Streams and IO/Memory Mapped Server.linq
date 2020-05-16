<Query Kind="Statements">
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
</Query>

using (MemoryMappedFile mmFile = MemoryMappedFile.CreateNew ("Demo", 500))
using (MemoryMappedViewAccessor accessor = mmFile.CreateViewAccessor())
{
	accessor.Write (0, 12345);
	
	// In LINQPad, manaully stop the query to exit
	Console.ReadLine();   // Keep shared memory alive until user hits Enter.
}