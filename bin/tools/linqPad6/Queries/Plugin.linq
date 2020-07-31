<Query Kind="Program">
  <Reference>C:\Users\Morteza Talebi\source\repos\BPA.Base\bin\Debug\netstandard2.0\BPA.Base.dll</Reference>
  <Namespace>BPA.Base</Namespace>
</Query>

void Main()
{
	var assembly = Assembly.LoadFrom("C:\\Users\\Morteza Talebi\\source\\repos\\MainApp\\bin\\Debug\\netcoreapp3.1\\BPA.Inventory.dll");

	Type refType = typeof(string);
	Type userType = null;
	foreach (Type type in assembly.GetTypes())
	{
		if (type.FullName == "BPA.Base.IReferencedEntity")
		{
			refType = type;
		}

		if (type.FullName == "BPA.Base.User")
		{
			userType = type;
		}
	}

	foreach (Type type in assembly.GetTypes())
	{
		if (type.GetInterfaces().Contains(typeof(IReferencedEntity)))
		{
			//Console.WriteLine(type.FullName);
		}

		if (type.GetInterfaces().Contains(typeof(IReferencedEntity)) && type.GetInterfaces().Contains(typeof(IUser)))
		{
			userType = type;



			foreach (var property in userType.GetProperties())
			{
				//Console.WriteLine("Name: " + property.Name);
				if (property.CustomAttributes.Select(ca => ca.AttributeType).Contains(typeof(UserAttribute)))
				{
					var x = property.CustomAttributes.First() as CustomAttributeData;
				}
			}
			Console.WriteLine();
		
		}
	}




}

// Define other methods, classes and namespaces here
