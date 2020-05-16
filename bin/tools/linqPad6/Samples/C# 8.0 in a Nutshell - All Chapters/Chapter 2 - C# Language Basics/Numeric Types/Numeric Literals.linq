<Query Kind="Statements" />

// Integral literals can use decimal or hexadecimal notation; hexadecimal is denoted with the 0x prefix:
int x = 127;
long y = 0x7F;

//From C# 7, you can insert an underscore anywhere inside a numeric literal to make it more readable:
int million = 1_000_000;

//C# 7 also lets you specify numbers in binary with the 0b prefix:
var b = 0b1010_1011_1100_1101_1110_1111;

//Real literals can use decimal and/or exponential notation. For example:
double d = 1.5;
double doubleMillion = 1E06;

// Numeric literal type inference:
Console.WriteLine (       1.0.GetType());  // Double  (double)
Console.WriteLine (      1E06.GetType());  // Double  (double)
Console.WriteLine (         1.GetType());  // Int32   (int)
Console.WriteLine (0xF0000000.GetType());  // UInt32  (uint)
Console.WriteLine (0x100000000.GetType());  // Int64   (long)