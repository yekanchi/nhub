<Query Kind="Expression">
  <Connection>
    <ID>5a0bef93-4afd-4663-9715-5c1f01b52cda</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

new XElement("books",
				from book in Books
				orderby book.Title
				select new XElement("book",
						new XElement("title", book.Title),
						new XElement("authors",
								from bookAuthor in book.BookAuthors
								orderby bookAuthor.AuthorOrder
								select new XElement("author",
										new XElement("firstName", bookAuthor.AuthorEntity.FirstName),
										new XElement("lastName", bookAuthor.AuthorEntity.LastName),
										new XElement("webSite", bookAuthor.AuthorEntity.WebSite)
								)
						),
						new XElement("subject",
								new XElement("name", book.SubjectEntity.Name),
								new XElement("description", book.SubjectEntity.Description)
						),
						new XElement("publisher", book.PublisherEntity.Name),
						new XElement("publicationDate", book.PubDate),
						new XElement("price", book.Price),
						new XElement("isbn", book.Isbn),
						new XElement("notes", book.Notes),
						new XElement("summary", book.Summary),
						new XElement("reviews",
						    from review in book.Reviews
						    orderby review.Rating
						    select new XElement("review",
										new XElement("user", review.UserEntity.Name),
										new XElement("rating", review.Rating),
										new XElement("comments", review.Comments)
								)
						)
				)
			)