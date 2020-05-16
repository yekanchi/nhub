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
		int x = 123, y = 234;

	}
}");

var references = ((string)AppContext.GetData ("TRUSTED_PLATFORM_ASSEMBLIES"))
	.Split (Path.PathSeparator)
	.Select (path => MetadataReference.CreateFromFile (path));

var compilation = CSharpCompilation.Create ("test")
	.AddReferences (references)
	.AddSyntaxTrees (tree);

SemanticModel model = compilation.GetSemanticModel (tree);

// Look for available symbols at start of 6th line:
int index = tree.GetText().Lines[5].Start;

foreach (ISymbol symbol in model.LookupSymbols (index))
	Console.WriteLine (symbol.ToString());

// If the following line throws an InvalidOperationException, you will need to update to the latest LINQPad beta.
model.LookupSymbols (index).Dump ("More detail", 1);