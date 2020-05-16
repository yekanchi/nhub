<Query Kind="Statements">
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
</Query>

File.WriteAllBytes ("long.bin", new byte [1000000]);

using MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile ("long.bin");
using MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();
accessor.Write (500000, (byte)77);
Console.WriteLine (accessor.ReadByte (500000));   // 77

File.WriteAllBytes ("short.bin", new byte [1]);
using MemoryMappedFile mmfSh = MemoryMappedFile.CreateFromFile
                   ("short.bin", FileMode.Create, null, 1000);
using MemoryMappedViewAccessor accessorSh = mmfSh.CreateViewAccessor();

accessorSh.Write (500, (byte)42);
Console.WriteLine (accessorSh.ReadByte (500));