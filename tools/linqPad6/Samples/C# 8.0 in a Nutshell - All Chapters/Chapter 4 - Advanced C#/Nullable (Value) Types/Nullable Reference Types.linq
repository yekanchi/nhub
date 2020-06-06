<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

#nullable enable
void Main()
{
	// Number and Photo must be assigned non-null values because we're
	// in an enabled nullable annotation context.
	var myLicense = new DriversLicense()
	{
		Number = "C12345",
		Photo = new Bitmap (512, 512) // A real system might load from a database or camera attached to the computer    
	};

	// Both of these can safely be derenced:
	Console.WriteLine (myLicense.Number.Length);   
	Console.WriteLine (myLicense.Photo.Height);
	// This violates CS8602 and will generate either a warning or error
	// depending on whether nullable annotation context related warnings
	// are configured to produce errors instead:
	Console.WriteLine (myLicense.Restrictions.Length);
	// By using the null forgiveness operator (!), we tell the compiler to
	// ignore its static flow analysis. We state we know better (and if 
	// we're wrong, a NullReferenceException results).
	Console.WriteLine (myLicense.Restrictions!.Length);
	myLicense.Restrictions = "None";
	// No warning/error since the compiler can prove a non-null assignment
	Console.WriteLine (myLicense.Restrictions.Length);
}

public class DriversLicense
{
	public string Number { get; set; }
	public Image Photo { get; set;}
	public string? Restrictions { get; set; }
}