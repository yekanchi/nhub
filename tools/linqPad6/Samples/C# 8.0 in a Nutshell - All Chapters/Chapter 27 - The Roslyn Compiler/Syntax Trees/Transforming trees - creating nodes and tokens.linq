<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
</Query>

QualifiedNameSyntax qualifiedName = SyntaxFactory.QualifiedName (
	SyntaxFactory.IdentifierName ("System"),
	SyntaxFactory.IdentifierName ("Text"));

UsingDirectiveSyntax usingDirective =
	SyntaxFactory.UsingDirective (qualifiedName);

Console.WriteLine (usingDirective.ToFullString());  // usingSystem.Text;

// Explicitly adding trivia:
usingDirective = usingDirective.WithUsingKeyword (
	usingDirective.UsingKeyword.WithTrailingTrivia (
	SyntaxFactory.Whitespace (" ")));

Console.WriteLine (usingDirective.ToFullString());  // using System.Text;

var existingTree = CSharpSyntaxTree.ParseText ("class Program {}");
var existingUnit = (CompilationUnitSyntax)existingTree.GetRoot();

var unitWithUsing = existingUnit.AddUsings (usingDirective);

var treeWithUsing = CSharpSyntaxTree.Create (unitWithUsing.NormalizeWhitespace());
treeWithUsing.ToString().Dump ("Final result");