<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c394</ID>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFile>true</AttachFile>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <DisplayName>Nutshell</DisplayName>
    <Persist>true</Persist>
    <DriverData>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	XElement project = XElement.Parse (@"<Project Sdk=""Microsoft.NET.Sdk"">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>netcoreapp3.0</TargetFramework>
		<Authors>Joe Bloggs</Authors>
		<Version>1.1.42</Version>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include=""Microsoft.EntityFrameworkCore"" 
            Version=""3.0.0"" />
		<PackageReference Include=""Microsoft.EntityFrameworkCore.Proxies"" 
            Version=""3.0.0"" />
		<PackageReference Include=""Microsoft.EntityFrameworkCore.SqlServer"" 
            Version=""3.0.0"" />
		<PackageReference Include=""Newtonsoft.Json"" 
            Version=""12.0.2"" />
	</ItemGroup>

</Project>
");

 IEnumerable<string> depNames =
	from compileItem in
         project.Elements ("ItemGroup").Elements ("PackageReference")
	let include = compileItem.Attribute ("Include")
	where include != null
	select include.Value;
	
	var query = new XElement ("Project", CreateHierarchy (depNames ));
	query.Dump();
}

static IEnumerable<XElement> CreateHierarchy (IEnumerable<string> depName)
{
	var brokenUp = from path in depName
                   let split = path.Split (new char[] { '.' }, 2)
                   orderby split [0]
                   select new
                   {
                   	name = split [0],
                   	remainder = split.ElementAtOrDefault (1)
                   };

	IEnumerable<XElement> pkg = from b in brokenUp
                                where b.remainder == null
                                select new XElement ("pkg", b.name);

	IEnumerable<XElement> parts = from b in brokenUp
                                  where b.remainder != null
                                  group b.remainder by b.name into grp
                                  select new XElement ("part",
                                  	new XAttribute ("name", grp.Key),
                                  	CreateHierarchy (grp)
                                  );
	return pkg.Concat (parts);
}