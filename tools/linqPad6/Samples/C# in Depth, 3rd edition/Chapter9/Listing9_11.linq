<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    PrintConvertedValue("I'm a string", x => x.Length);
}

static void PrintConvertedValue<TInput, TOutput>
    (TInput input, Converter<TInput, TOutput> converter)
{
    Console.WriteLine(converter(input));
}
