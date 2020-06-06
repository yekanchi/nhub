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

var unit = SyntaxFactory.CompilationUnit().AddUsings (usingDirective);

// Create a simple empty class definition:
unit = unit.AddMembers (SyntaxFactory.ClassDeclaration ("Program"));

var tree = CSharpSyntaxTree.Create (unit.NormalizeWhitespace());
Console.WriteLine (tree.ToString());