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
	static void Main() => System.Console.WriteLine (123);
}");

var references = ((string)AppContext.GetData ("TRUSTED_PLATFORM_ASSEMBLIES"))
	.Split (Path.PathSeparator)
	.Select (path => MetadataReference.CreateFromFile (path));

var compilation = CSharpCompilation.Create ("test")
	.AddReferences (references)
	.AddSyntaxTrees (tree);

SemanticModel model = compilation.GetSemanticModel (tree);

var mainMethod = tree.GetRoot().DescendantTokens().Single (
	t => t.Text == "Main").Parent;

SymbolInfo symbolInfo = model.GetSymbolInfo (mainMethod);
Console.WriteLine (symbolInfo.Symbol == null);              // True
Console.WriteLine (symbolInfo.CandidateSymbols.Length);     // 0

ISymbol symbol = model.GetDeclaredSymbol (mainMethod);
symbol.Dump(1);