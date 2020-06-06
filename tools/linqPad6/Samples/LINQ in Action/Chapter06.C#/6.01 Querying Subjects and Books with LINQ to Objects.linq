<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
</Query>

void Main()
{
	//6-1: Querying Subjects and Books with LINQ to Objects
	IEnumerable<Book> books = Book.GetBooks();
	IEnumerable<Subject> subjects = Subject.GetSubjects();

	  var query = from subject in subjects
				  join book in books on subject.SubjectId equals book.SubjectId
				  where book.Price < 30
				  orderby subject.Description, book.Title
				  select new
				  {
					subject.Description,
					book.Title,
					book.Price
				  };
	query.Dump();
}

// Define other methods and classes here
	  public class Book
	  {
		public Guid BookId { get; set; }
		public String Isbn { get; set; }
		public string Notes { get; set; }
		public Int32 PageCount { get; set; }
		public Decimal Price { get; set; }
		public DateTime PublicationDate { get; set; }
		public String Summary { get; set; }
		public String Title { get; set; }
		public Guid SubjectId { get; set; }
		public Guid PublisherId { get; set; }
	
		public static IEnumerable<Book> GetBooks()
		{
		  List<Book> books = new List<Book>();
		  using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\lia.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
		  {
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT ID, Isbn, Notes, PageCount, Price, PubDate, Publisher, Subject, Summary, Title FROM dbo.Book";
			using (SqlDataReader reader = command.ExecuteReader())
			{
			  while (reader.Read())
			  {
				Book newBook = new Book();
				if (!reader.IsDBNull(0)) { newBook.BookId = reader.GetGuid(0); }
				if (!reader.IsDBNull(1)) { newBook.Isbn = reader.GetString(1); }
				if (!reader.IsDBNull(2)) { newBook.Notes = reader.GetString(2); }
				if (!reader.IsDBNull(3)) { newBook.PageCount = reader.GetInt32(3); }
				if (!reader.IsDBNull(4)) { newBook.Price = reader.GetDecimal(4); }
				if (!reader.IsDBNull(5)) { newBook.PublicationDate = reader.GetDateTime(5); }
				if (!reader.IsDBNull(6)) { newBook.PublisherId = reader.GetGuid(6); }
				if (!reader.IsDBNull(7)) { newBook.SubjectId = reader.GetGuid(7); }
				if (!reader.IsDBNull(8)) { newBook.Summary = reader.GetString(8); }
				if (!reader.IsDBNull(9)) { newBook.Title = reader.GetString(9); }
				books.Add(newBook);
			  }
			}
		  }
		  return books;
		}
	
	  }

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