<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    DateTime stop = DateTime.Now.AddSeconds(2);
    foreach (int i in CountWithTimeLimit(stop))
    {
        Console.WriteLine("Received {0}", i);
        Thread.Sleep(300);
    }    
}

static IEnumerable<int> CountWithTimeLimit(DateTime limit)
{
    for (int i = 1; i <= 100; i++)
    {
        if (DateTime.Now >= limit)
        {
            yield break;
        }
        yield return i;
    }
}
