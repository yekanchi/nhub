<Query Kind="Statements">
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
</Query>

// This can run in a separate executable:
using MemoryMappedFile mmFile = MemoryMappedFile.OpenExisting ("Demo");
using MemoryMappedViewAccessor accessor = mmFile.CreateViewAccessor();

Console.WriteLine (accessor.ReadInt32 (0));   // 12345
