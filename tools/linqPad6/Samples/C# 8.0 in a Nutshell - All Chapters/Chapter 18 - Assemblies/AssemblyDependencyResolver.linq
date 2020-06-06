<Query Kind="Statements">
  <Namespace>System.Runtime.Loader</Namespace>
</Query>

var resolver = new AssemblyDependencyResolver (@"c:\PathToSomeAssemblyWithDepsDotJson.dll");
string path = resolver.ResolveAssemblyToPath (new AssemblyName ("someDependentAssembly"));
