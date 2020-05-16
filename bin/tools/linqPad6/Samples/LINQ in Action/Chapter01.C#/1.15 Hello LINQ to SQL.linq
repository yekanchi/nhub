<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Reference Assemblies\Microsoft\Framework\v3.5\System.Data.Linq.dll</Reference>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

[Table(Name = "Contacts")]
class Contact
{
  [Column(IsPrimaryKey=true)]
  public int ContactID { get; set; }
  [Column(Name="ContactName")]
  public string Name { get; set; }
  [Column]
  public string City { get; set; }
}

static void Main()
{
  // Get access to the database
  string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\LINQPad\Samples\LINQ in Action\NORTHWND.mdf";
  DataContext db = new DataContext(path);

  // Query for contacts from Paris
  var contacts =
  from contact in db.GetTable<Contact>()
	  where contact.City == "Paris"
	  select contact;

  // Display the list of matching contacts
  foreach (var contact in contacts)
    Console.WriteLine("Bonjour " + contact.Name);
}