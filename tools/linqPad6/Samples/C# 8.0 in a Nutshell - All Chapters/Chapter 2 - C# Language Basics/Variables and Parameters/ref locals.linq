<Query Kind="Statements" />

// C# 7 added an esoteric feature, whereby you can define a local variable that references
// an element in an array or field in an object. 
int[] numbers = { 0, 1, 2, 3, 4 };
ref int numRef = ref numbers [2];

// In this example, numRef is a reference to the numbers [2].When we modify numRef,
// we modify the array element:
numRef *= 10;
Console.WriteLine (numRef);        // 20
Console.WriteLine (numbers [2]);   // 20