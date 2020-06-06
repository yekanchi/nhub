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

root.DescendantNodes()
	.First (m => m.Kind() == SyntaxKind.MethodDeclaration)
	.Dump(1);

root.DescendantNodes()
	.OfType<MethodDeclarationSyntax>()
	.Single()
	.Dump(1);
	
root.DescendantNodes()
	.OfType<MethodDeclarationSyntax>()
	.Single (m => m.Identifier.Text == "Main")
	.Dump(1);

root.DescendantNodes()
	.First (m =>
			m.Kind() == SyntaxKind.MethodDeclaration &&
			m.ChildTokens().Any (t => t.Kind() == SyntaxKind.IdentifierToken && t.Text == "Main"))
	 .Dump(1);