<Query Kind="Statements">
   <NuGetReference>Roslyn.Compilers.CSharp</NuGetReference>
   <Namespace>Roslyn.Compilers.CSharp</Namespace>
</Query>

			
			/*
			
				To Run this code you shall need to add reference of Roslyn.Compilers.CSharp
				You can download Roslyn from http://www.microsoft.com/en-in/download/details.aspx?id=34685
			*/
			
			SyntaxTree tree = SyntaxTree.ParseText(@"int fun(int x){ int y = 0; x++; return x+1;} 
                                                     double funny(double x){ return x/2.13;}");

            List<MethodDeclarationSyntax> methods = tree.GetRoot()
                                                         .DescendantNodes()
                                                         .Where(d => d.Kind == SyntaxKind.MethodDeclaration)
                                                         .Cast<MethodDeclarationSyntax>()
                                                         .ToList();
            int lvc = methods[0].Body.Statements.Count(x => x.Kind == SyntaxKind.LocalDeclarationStatement);
            methods.Select(z => new { MethodName = z.Identifier.ValueText, LoC = z.Body.Statements.Count })
                .OrderByDescending(x => x.LoC)
                .ToList()
                .ForEach(x => Console.WriteLine(x.MethodName + " " + x.LoC));