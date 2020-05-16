<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    ConvertTwice("Another string",
                text => text.Length,
                length => Math.Sqrt(length));    
}

static void ConvertTwice<TInput, TMiddle, TOutput> (TInput input,
                                                    Converter<TInput, TMiddle> firstConversion,
                                                    Converter<TMiddle, TOutput> secondConversion)
{
    TMiddle middle = firstConversion(input);
    TOutput output = secondConversion(middle);
    Console.WriteLine(output);
}
