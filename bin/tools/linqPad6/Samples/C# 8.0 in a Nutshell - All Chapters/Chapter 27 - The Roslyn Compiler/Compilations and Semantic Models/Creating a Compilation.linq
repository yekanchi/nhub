<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
</Query>

var compilation = CSharpCompilation.Create ("test");

compilation = compilation.WithOptions (
	new CSharpCompilationOptions (OutputKind.ConsoleApplication));

var tree = CSharpSyntaxTree.ParseText (@"class Program 
{
	static void Main() => System.Console.WriteLine (""Hello"");
}");

compilation = compilation.AddSyntaxTrees (tree);

string trustedAssemblies = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES");
var trustedAssemblyPaths = trustedAssemblies.Split (Path.PathSeparator);
var references = trustedAssemblyPaths.Select (path => MetadataReference.CreateFromFile (path));

compilation = compilation.AddReferences (references);

// Or, in one step:

compilation = CSharpCompilation
	.Create ("test")
	.WithOptions (new CSharpCompilationOptions (OutputKind.ConsoleApplication))
	.AddSyntaxTrees (tree)
	.AddReferences (references);
	
compilation.GetDiagnostics().Dump ("Errors and warnings");