<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Emit</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

var tree = CSharpSyntaxTree.ParseText (@"class Program 
{
	static void Main() => System.Console.WriteLine (""Hello"");
}");

string trustedAssemblies = (string)AppContext.GetData ("TRUSTED_PLATFORM_ASSEMBLIES");
var trustedAssemblyPaths = trustedAssemblies.Split (Path.PathSeparator);
var references = trustedAssemblyPaths.Select (path => MetadataReference.CreateFromFile (path));

var compilation = CSharpCompilation
	.Create ("test")
	.WithOptions (new CSharpCompilationOptions (OutputKind.ConsoleApplication))
	.AddSyntaxTrees (tree)
	.AddReferences (references);

string outputPath = "test.dll";
EmitResult result = compilation.Emit (outputPath);
Console.WriteLine (result.Success);

// Execute the program we just compiled.
Util.Cmd (@"dotnet.exe", "\"" + outputPath + "\"");