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
		System.Diagnostics.Debug.WriteLine('g');
		data.myId = System.Guid.NewGuid();
		return data.somevar;
		
	}
	";

	// data to be sent into the script
	dynamic expando = new ExpandoObject();
	expando.myId = myId;
	//expando.Y = 45;
	(expando as IDictionary<string, Object>).Add("somevar", new someClass());


	// setup references needed
	var refs = new List<MetadataReference>
	{
		MetadataReference.CreateFromFile(typeof(System.Guid).GetTypeInfo().Assembly.Location),
		MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).GetTypeInfo().Assembly.Location),
		MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.DynamicAttribute).GetTypeInfo().Assembly.Location)
	};

	var script = CSharpScript.Create(scriptContent,
	options: ScriptOptions.Default.AddReferences(refs),
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

public class someClass
{
	/// <summary>User Name</summary>
	public int cost { get; set; }
	public int value => 25;
	public string somestring => "Some test string";
}