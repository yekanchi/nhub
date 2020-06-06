<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
</Query>

var tree = CSharpSyntaxTree.ParseText (@"#define FOO");
SyntaxNode root = tree.GetRoot();

Console.WriteLine (root.ContainsDirectives);      // True

// directive is the root node of the structured trivia:
var directive = root.GetFirstDirective();
Console.WriteLine (directive.Kind());             // DefineDirectiveTrivia
Console.WriteLine (directive.ToString());         // #define FOO

// If there were more directives, we could get to them as follows:
Console.WriteLine (directive.GetNextDirective());    // (null)

var hashDefine = (DefineDirectiveTriviaSyntax)root.GetFirstDirective();
Console.WriteLine (hashDefine.Name.Text);     // FOO
