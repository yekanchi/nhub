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

	var whiteWalker = new WhiteWalker ();
	whiteWalker.Visit (root);
	whiteWalker.SpaceCount.Dump ("spaces");
}

class WhiteWalker : CSharpSyntaxWalker   // Counts space characters
{
	public int SpaceCount { get; private set; }

	public WhiteWalker() : base (SyntaxWalkerDepth.Trivia) { }

	public override void VisitTrivia (SyntaxTrivia trivia)
	{
		SpaceCount += trivia.ToString().Count (char.IsWhiteSpace);
		base.VisitTrivia (trivia);
	}
}