<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    MethodInvoker x = CreateDelegateInstance();
    x();
    x();    
}

static MethodInvoker CreateDelegateInstance()
{
    int counter = 5;

    MethodInvoker ret = delegate
    {
         Console.WriteLine(counter);
         counter++;
    };
    ret();
    return ret;
}

