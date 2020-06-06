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

var cds = (ClassDeclarationSyntax) root.ChildNodes().Single();

root.DescendantTokens()
	.Select (t => new { Kind = t.Kind(), t.Text })
	.Dump();