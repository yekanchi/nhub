<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
</Query>

var tree = CSharpSyntaxTree.ParseText (@"class Test
{
	static void Main() => Console.WriteLine (""Hello"");
}");

SyntaxNode root = tree.GetRoot();
Console.WriteLine (root.GetType().Name);   // CompilationUnitSyntax