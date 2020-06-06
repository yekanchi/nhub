<Query Kind="Program">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp.Scripting</NuGetReference>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Scripting</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Scripting</Namespace>
</Query>

void Main()
{
	// for sending in parameters to the script
	var myId = Guid.NewGuid();


	// Script that will use dynamic
	var scriptContent = @"
	{
		data.myId = Guid.NewGuid(); 
		return data.myId == Guid.Empty;
	}
	";

	// data to be sent into the script
	dynamic expando = new ExpandoObject();
	expando.myId = myId;
	//expando.Y = 45;



	// setup references needed
	var refs = new List<MetadataReference>
	{
		MetadataReference.CreateFromFile(typeof(System.Guid).GetTypeInfo().Assembly.Location),
		MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).GetTypeInfo().Assembly.Location),
		MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.DynamicAttribute).GetTypeInfo().Assembly.Location)
	};
	
	var script = CSharpScript.Create(scriptContent, 
	options: ScriptOptions.Default.AddReferences(refs).WithReferences(typeof(System.Guid).Assembly).WithImports("System.Guid"), 
	globalsType: typeof(Globals));

	script.Compile();

	// create new global that will contain the data we want to send into the script
	var g = new Globals() { data = expando };

	//Execute and display result
	var r = script.RunAsync(g).Result;
	Debug.WriteLine(r.Script.Code);
	Console.WriteLine(r.ReturnValue);
}

public class Globals
{
	public dynamic data;
}
