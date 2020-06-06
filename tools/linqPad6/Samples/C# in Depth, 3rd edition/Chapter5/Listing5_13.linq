<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

List<MethodInvoker> list = new List<MethodInvoker>();

for (int index = 0; index < 5; index++)
{
    int counter = index * 10;
    list.Add(delegate
    {
        Console.WriteLine(counter);
        counter++;
    });
}

foreach (MethodInvoker t in list)
{
    t();
}

list[0]();
list[0]();
list[0]();

list[1]();