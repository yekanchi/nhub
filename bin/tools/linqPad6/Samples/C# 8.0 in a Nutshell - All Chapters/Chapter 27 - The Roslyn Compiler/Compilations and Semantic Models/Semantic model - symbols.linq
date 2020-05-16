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

var writeLineNode = tree.GetRoot().DescendantTokens().Single (
	 t => t.Text == "WriteLine").Parent;

ISymbol symbol = model.GetSymbolInfo (writeLineNode).Symbol;

Console.WriteLine (symbol.Name);                   // WriteLine
Console.WriteLine (symbol.Kind);                   // Method
Console.WriteLine (symbol.IsStatic);               // True
Console.WriteLine (symbol.ContainingType.Name);    // Console

var method = (IMethodSymbol)symbol;
Console.WriteLine (method.ReturnType.ToString());  // void

Console.WriteLine (symbol.Language);                // C#

var location = symbol.Locations.First();
Console.WriteLine (location.Kind);                       // MetadataFile
Console.WriteLine (location.MetadataModule 
                   == compilation.References.Single());  // Trye
Console.WriteLine (location.SourceTree == null);         // True
Console.WriteLine (location.SourceSpan);                 // [0..0)

symbol.ContainingType.GetMembers ("WriteLine").OfType<IMethodSymbol>()
	.Select (m => m.ToString()).Dump();