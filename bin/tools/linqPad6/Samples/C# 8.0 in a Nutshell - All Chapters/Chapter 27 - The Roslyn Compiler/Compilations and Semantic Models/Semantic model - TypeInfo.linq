<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Emit</Namespace>
</Query>

var tree = CSharpSyntaxTree.ParseText (@"class Program 
{
	static void Main()
	{
		var now = System.DateTime.Now;
		System.Console.WriteLine (now - now);
	}
}");

var references = ((string)AppContext.GetData ("TRUSTED_PLATFORM_ASSEMBLIES"))
	.Split (Path.PathSeparator)
	.Select (path => MetadataReference.CreateFromFile (path));

var compilation = CSharpCompilation.Create ("test")
	.AddReferences (references)
	.AddSyntaxTrees (tree);

SemanticModel model = compilation.GetSemanticModel (tree);

SyntaxNode binaryExpr = tree.GetRoot().DescendantTokens().Single (
	t => t.Text == "-").Parent;

var typeInfo = model.GetTypeInfo (binaryExpr);

Console.WriteLine (typeInfo.Type.ToString());             // System.TimeSpan
Console.WriteLine (typeInfo.ConvertedType.ToString());    // object