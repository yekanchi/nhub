<Query Kind="Program">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Emit</Namespace>
</Query>

void Main()
{
	var tree = CSharpSyntaxTree.ParseText (@"class Program
{
	static Program() {}
	public Program() {}

	static void Main()
	{
		Program p = new Program();
		p.Foo();
	}

	static void Foo() => Bar();
	static void Bar() => Foo();	
}
");

	var references = ((string)AppContext.GetData ("TRUSTED_PLATFORM_ASSEMBLIES"))
		.Split (Path.PathSeparator)
		.Select (path => MetadataReference.CreateFromFile (path));

	var compilation = CSharpCompilation.Create ("test")
		.AddReferences (references)
		.AddSyntaxTrees (tree);

	var model = compilation.GetSemanticModel (tree);

	var tokens = tree.GetRoot().DescendantTokens();

	// Rename the Program class to Program2:
	SyntaxToken program = tokens.First (t => t.Text == "Program");
	Console.WriteLine (RenameSymbol (model, program, "Program2").ToString());

	// Rename the Foo method to Foo2:
	SyntaxToken foo = tokens.Last (t => t.Text == "Foo");
	Console.WriteLine (RenameSymbol (model, foo, "Foo2").ToString());

	// Rename the p local variable to p2:
	SyntaxToken p = tokens.Last (t => t.Text == "p");
	Console.WriteLine (RenameSymbol (model, p, "p2").ToString());
}

public SyntaxTree RenameSymbol (SemanticModel model, SyntaxToken token,
								string newName)
{
	IEnumerable<TextSpan> renameSpans =
		GetRenameSpans (model, token).OrderBy (s => s);

	SourceText newSourceText = model.SyntaxTree.GetText().WithChanges (
		renameSpans.Select (s => new TextChange (s, newName)));

	return model.SyntaxTree.WithChangedText (newSourceText);
}

public IEnumerable<TextSpan> GetRenameSpans (SemanticModel model,
											 SyntaxToken token)
{
	var node = token.Parent;

	ISymbol symbol =
		model.GetSymbolInfo (node).Symbol ??
		model.GetDeclaredSymbol (node);

	if (symbol == null) return null;   // No symbol to rename.

	var definitions =
		from location in symbol.Locations
		where location.SourceTree == node.SyntaxTree
		select location.SourceSpan;

	var usages =
		from t in model.SyntaxTree.GetRoot().DescendantTokens ()
		where t.Text == symbol.Name
		let s = model.GetSymbolInfo (t.Parent).Symbol
		where s == symbol
		select t.Span;

	if (symbol.Kind != SymbolKind.NamedType)
		return definitions.Concat (usages);

	var structors =
		from type in model.SyntaxTree.GetRoot().DescendantNodes()
											 .OfType<TypeDeclarationSyntax>()
		where type.Identifier.Text == symbol.Name
		let declaredSymbol = model.GetDeclaredSymbol (type)
		where declaredSymbol == symbol
		from method in type.Members
		let constructor = method as ConstructorDeclarationSyntax
		let destructor = method as DestructorDeclarationSyntax
		where constructor != null || destructor != null
		let identifier = constructor?.Identifier ?? destructor.Identifier
		select identifier.Span;

	return definitions.Concat (usages).Concat (structors);
}