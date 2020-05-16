<Query Kind="Program">
  <NuGetReference>DynamicLanguageRuntime</NuGetReference>
  <NuGetReference>IronPython</NuGetReference>
  <Namespace>IronPython.Hosting</Namespace>
  <Namespace>Microsoft.Scripting.Hosting</Namespace>
</Query>

static void Main()
{
	int result = (int) Calculate ("2 * 3");
	Console.WriteLine (result);              // 6
	
	var list = (IEnumerable) Calculate ("[1, 2, 3] + [4, 5]");
	foreach (int n in list) Console.Write (n);  // 12345
}
	
static object Calculate (string expression)
{
	ScriptEngine engine = Python.CreateEngine();
	return engine.Execute (expression);
}