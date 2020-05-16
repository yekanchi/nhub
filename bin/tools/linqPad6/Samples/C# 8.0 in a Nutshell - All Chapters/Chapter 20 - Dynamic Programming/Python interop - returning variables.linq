<Query Kind="Statements">
  <NuGetReference>DynamicLanguageRuntime</NuGetReference>
  <NuGetReference>IronPython</NuGetReference>
  <Namespace>IronPython.Hosting</Namespace>
  <Namespace>Microsoft.Scripting</Namespace>
  <Namespace>Microsoft.Scripting.Hosting</Namespace>
</Query>

string code = "result = input * 3";

ScriptEngine engine = Python.CreateEngine();

ScriptScope scope = engine.CreateScope();
scope.SetVariable ("input", 2);

ScriptSource source = engine.CreateScriptSourceFromString (code, SourceCodeKind.SingleStatement);
source.Execute (scope);
Console.WriteLine (scope.GetVariable ("result"));   // 6
