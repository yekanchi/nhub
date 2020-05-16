<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
</Query>

void Main()
{
	var home = new Address { Street = "1 Main St.", PostCode = "11235" };
	var work = new Address { Street = "4 Elm Ln.", PostCode = "31415" };
	
	var p = new Person { Name = "Ian", HomeAddress = home, WorkAddress = work };

	Console.WriteLine (JsonSerializer.Serialize (p,
                     new JsonSerializerOptions { WriteIndented = true }));
}

public class Address
{
	public string Street { get; set; }
	public string PostCode { get; set; }
}

public class Person
{
	public string Name { get; set; }
	public Address HomeAddress { get; set; }
	public Address WorkAddress { get; set; }
}