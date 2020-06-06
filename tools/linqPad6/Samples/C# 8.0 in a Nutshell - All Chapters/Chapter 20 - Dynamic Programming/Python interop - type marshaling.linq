<Query Kind="Statements">
  <NuGetReference>DynamicLanguageRuntime</NuGetReference>
  <NuGetReference>IronPython</NuGetReference>
  <Namespace>IronPython.Hosting</Namespace>
  <Namespace>Microsoft.Scripting</Namespace>
  <Namespace>Microsoft.Scripting.Hosting</Namespace>
</Query>

string code = @"sb.Append (""World"")";

ScriptEngine engine = Python.CreateEngine ();

ScriptScope scope = engine.CreateScope ();
var sb = new StringBuilder ("Hello");
scope.SetVariable ("sb", sb);

ScriptSource source = engine.CreateScriptSourceFromString (code, SourceCodeKind.SingleStatement);
source.Execute (scope);
string s = sb.ToString();
s.Dump ("StringBuilder");