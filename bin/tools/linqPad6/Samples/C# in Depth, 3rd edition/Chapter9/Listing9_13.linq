<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    WriteResult(delegate
    {
        if (DateTime.Now.Hour < 12)
        {
            return 10;
        }
        else
        {
            return new object();
        }
    });    
}

delegate T MyFunc<T>();

static void WriteResult<T>(MyFunc<T> function)
{
    Console.WriteLine(function());
}
