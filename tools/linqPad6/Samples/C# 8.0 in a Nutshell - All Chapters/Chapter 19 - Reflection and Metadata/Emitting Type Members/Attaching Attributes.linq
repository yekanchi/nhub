<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

// [XmlElement ("FirstName", Namespace="http://test/", Order=3)]

AssemblyName aname = new AssemblyName ("MyEmissions");

AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly (
	aname, AssemblyBuilderAccess.Run);

ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule ("MainModule");

TypeBuilder tb = modBuilder.DefineType ("Widget", TypeAttributes.Public);

Type attType = typeof (XmlElementAttribute);

ConstructorInfo attConstructor = attType.GetConstructor (
	new Type[] { typeof (string) } );

var att = new CustomAttributeBuilder (
	attConstructor,                        // Constructor
	new object[] { "FirstName" },          // Constructor arguments
	new PropertyInfo[] 
	{
		attType.GetProperty ("Namespace"),   // Properties
		attType.GetProperty ("Order")
	},
	new object[] { "http://test/", 3 }     // Property values
);

FieldBuilder myFieldBuilder = tb.DefineField ("SomeField", typeof (string),
                                      FieldAttributes.Public);
                                      
myFieldBuilder.SetCustomAttribute (att);
// or propBuilder.SetCustomAttribute (att);
// or typeBuilder.SetCustomAttribute (att);  etc

Type t = tb.CreateType();
t.GetField ("SomeField").GetCustomAttributes().Dump();