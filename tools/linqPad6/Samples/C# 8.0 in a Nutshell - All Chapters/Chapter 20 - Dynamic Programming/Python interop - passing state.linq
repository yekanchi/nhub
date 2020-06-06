<Query Kind="Statements">
  <NuGetReference>DynamicLanguageRuntime</NuGetReference>
  <NuGetReference>IronPython</NuGetReference>
  <Namespace>IronPython.Hosting</Namespace>
  <Namespace>Microsoft.Scripting</Namespace>
  <Namespace>Microsoft.Scripting.Hosting</Namespace>
</Query>

// The following string could come from a file or database:
string auditRule = "taxPaidLastYear / taxPaidThisYear > 2";

ScriptEngine engine = Python.CreateEngine ();    

ScriptScope scope = engine.CreateScope ();        
scope.SetVariable ("taxPaidLastYear", 20000m);
scope.SetVariable ("taxPaidThisYear", 8000m);

ScriptSource source = engine.CreateScriptSourceFromString (auditRule, SourceCodeKind.Expression);

bool auditRequired = (bool) source.Execute (scope);
Console.WriteLine (auditRequired);   // True