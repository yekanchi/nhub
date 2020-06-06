<Query Kind="Statements">
    <NuGetReference>Roslyn.Compilers.CSharp</NuGetReference>
    <Namespace>Roslyn.Compilers.CSharp</Namespace>
</Query>

            SyntaxTree tree = SyntaxTree.ParseText(@"int fun(int x){ int y = 0; x++; return x+1;} 
                                                                      double funny(double x){ return x/2.13;}");

            List<MethodDeclarationSyntax> methods = tree.GetRoot()
                                                         .DescendantNodes()
                                                         .Where(d => d.Kind == SyntaxKind.MethodDeclaration)
                                                         .Cast<MethodDeclarationSyntax>().ToList();
            methods.Select(z => new { MethodName = z.Identifier.ValueText, NBLocal = z.Body.Statements.Count(x => x.Kind == SyntaxKind.LocalDeclarationStatement) })
                   .OrderByDescending(x => x.NBLocal)
                   .ToList()
                   .ForEach(x => Console.WriteLine(x.MethodName + " " + x.NBLocal));