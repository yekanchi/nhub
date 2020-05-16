<Query Kind="Program">
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
</Query>

void Main()
{
	File.WriteAllBytes ("unsafe.bin", new byte [100]);

	var data = new Data { X = 123, Y = 456 };

	using MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile ("unsafe.bin");
	using MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();

	accessor.Write (0, ref data);
	accessor.Read (0, out data);
	Console.WriteLine (data.X + " " + data.Y);   // 123 456

	unsafe
	{
		byte* pointer = null;
		try
		{
			accessor.SafeMemoryMappedViewHandle.AcquirePointer (ref pointer);
			int* intPointer = (int*)pointer;
			Console.WriteLine (*intPointer);               // 123
		}
		finally
		{
			if (pointer != null)
				accessor.SafeMemoryMappedViewHandle.ReleasePointer();
		}
	}
}

struct Data { public int X, Y; }