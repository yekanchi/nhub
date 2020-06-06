<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
</Query>

static void Main()
{
	IEnumerable<Subject> subjects = Subject.GetSubjects();
	subjects.Dump();
}

// Define other methods and classes here
 public class Subject
  {
	public Guid SubjectId { get; set; }
	public String Description { get; set; }
	public String Name { get; set; }

	public static IEnumerable<Subject> GetSubjects()
	{
	  List<Subject> subjects = new List<Subject>();
	  using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\lia.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
	  {
		connection.Open();
		SqlCommand command = connection.CreateCommand();
		command.CommandText = "SELECT ID, Name, Description FROM dbo.Subject";
		using (SqlDataReader reader = command.ExecuteReader())
		{
		  while (reader.Read())
		  {
			Subject newSubject = new Subject();
			if (!reader.IsDBNull(0)) { newSubject.SubjectId = reader.GetGuid(0); }
			if (!reader.IsDBNull(1)) { newSubject.Description = reader.GetString(1); }
			if (!reader.IsDBNull(2)) { newSubject.Name = reader.GetString(2); }
			subjects.Add(newSubject);
		  }
		}
	  }
	  return subjects;
	}
  }