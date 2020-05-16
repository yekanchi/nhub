<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    SortAndShowFiles("Sorted by name:", delegate(FileInfo first, FileInfo second)
        { return first.Name.CompareTo(second.Name); }
    );
    
    
    SortAndShowFiles("Sorted by length:", delegate(FileInfo first, FileInfo second)
        { return first.Length.CompareTo(second.Length); }
    );    
}

static void SortAndShowFiles(string title,
                             Comparison<FileInfo> sortOrder)
{
    FileInfo[] files = new DirectoryInfo(@"C:\").GetFiles();

    Array.Sort(files, sortOrder);

    Console.WriteLine(title);
    foreach (FileInfo file in files)
    {
        Console.WriteLine("  {0} ({1} bytes)",
                           file.Name, file.Length);
    }
}
