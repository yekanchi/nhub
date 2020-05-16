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
		int xyz = 123;
	}
}");

var references = ((string)AppContext.GetData ("TRUSTED_PLATFORM_ASSEMBLIES"))
	.Split (Path.PathSeparator)
	.Select (path => MetadataReference.CreateFromFile (path));

var compilation = CSharpCompilation.Create ("test")
	.AddReferences (references)
	.AddSyntaxTrees (tree);

SemanticModel model = compilation.GetSemanticModel (tree);

SyntaxNode variableDecl = tree.GetRoot().DescendantTokens().Single (
	t => t.Text == "xyz").Parent;

var local = (ILocalSymbol) model.GetDeclaredSymbol (variableDecl);
Console.WriteLine (local.Type.ToString());             // int
Console.WriteLine (local.Type.BaseType.ToString());    // System.ValueType

// If the following line throws an InvalidOperationException, you will need to update to the latest LINQPad beta.
local.Type.Dump ("More detail", 1);