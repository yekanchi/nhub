<Query Kind="Program">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
</Query>

void Main()
{
	var tree = CSharpSyntaxTree.ParseText (@"class Test
{
	static void Main()
	{
		if (true)
		if (true);
	};
}");

	SyntaxNode root = tree.GetRoot();

	var ifCounter = new IfCounter ();
	ifCounter.Visit (root);
	Console.WriteLine ($"I found {ifCounter.IfCount} if statements");

	root.DescendantNodes().OfType<IfStatementSyntax>().Count().Dump ("Functional equivalent");
}

class IfCounter : CSharpSyntaxWalker
{
	public int IfCount { get; private set; }

	public override void VisitIfStatement (IfStatementSyntax node)
	{
		IfCount++;
		// Call the base method if you want to descend into children.
		base.VisitIfStatement (node);
	}
}