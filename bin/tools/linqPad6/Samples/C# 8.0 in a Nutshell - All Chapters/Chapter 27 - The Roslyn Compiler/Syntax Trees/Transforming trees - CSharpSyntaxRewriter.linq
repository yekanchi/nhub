<Query Kind="Program">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
</Query>

void Main()
{
	var tree = CSharpSyntaxTree.ParseText (@"class Program
{
	static void Main() { Test(); }
	static void Test() {         }
}");

	var rewriter = new MyRewriter();
	var newRoot = rewriter.Visit (tree.GetRoot());
	Console.WriteLine (newRoot.ToFullString());
}

class MyRewriter : CSharpSyntaxRewriter
{
	public override SyntaxNode VisitMethodDeclaration
		(MethodDeclarationSyntax node)
	{
		// "Replace" the method’s identifier with an uppercase version:
		return node.WithIdentifier (
			SyntaxFactory.Identifier (
			node.Identifier.LeadingTrivia,            // Preserve old trivia
			node.Identifier.Text.ToUpperInvariant(),
			node.Identifier.TrailingTrivia));         // Preserve old trivia
	}
}