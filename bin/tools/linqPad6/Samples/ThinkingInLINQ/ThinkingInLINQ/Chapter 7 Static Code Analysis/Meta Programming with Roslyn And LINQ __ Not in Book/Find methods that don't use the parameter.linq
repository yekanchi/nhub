<Query Kind="Statements">
  <NuGetReference>Roslyn.Compilers.CSharp</NuGetReference>
  <Namespace>Roslyn.Compilers.CSharp</Namespace>
</Query>

            SyntaxTree tree = SyntaxTree.ParseText(@"int fun(int x,int z){ int y = 0; x++; return x+1;} 
                                                   double funny(double x){ return x/2.13;}");

            List<MethodDeclarationSyntax> methods = tree.GetRoot()
                                                         .DescendantNodes()
                                                         .Where(d => d.Kind == SyntaxKind.MethodDeclaration)
                                                         .Cast<MethodDeclarationSyntax>().ToList();
            methods.Select(z =>
                  {
                      var parameters = z.ParameterList.Parameters.Select(p => p.Identifier.ValueText);
                      return
                          new
                          {
                              MethodName = z.Identifier.ValueText,
                              IsUsingAllParameter = parameters.All(x => z.Body.GetText().ToString().Contains(x))
                          };
                  })
                  .Where(x => !x.IsUsingAllParameter)
                   .ToList()
                   .ForEach(x => Console.WriteLine(x.MethodName + " " + x.IsUsingAllParameter)); 