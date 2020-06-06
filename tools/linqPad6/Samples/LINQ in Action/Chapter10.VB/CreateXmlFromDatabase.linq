<Query Kind="VBStatements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim xml As XElement = <books>
					<%= From book In Books _
						Select <book> 
									<authors>
											 <%= From bookAuthor In book.BookAuthors _
												 Order By bookAuthor.AuthorOrder _
												 Select <author>
																	<firstName><%= bookAuthor.AuthorEntity.FirstName %></firstName>
																	<lastName><%= bookAuthor.AuthorEntity.LastName %></lastName>
																	<website><%= bookAuthor.AuthorEntity.WebSite %></website>
																</author> %>
									 </authors>
									 <subject>
										 <name><%= book.SubjectEntity.Name %></name>
										 <description><%= book.SubjectEntity.Description %></description>
									 </subject>
									 <publisher><%= book.PublisherEntity.Name %></publisher>
									 <publicationDate><%= book.PubDate %></publicationDate>
									 <price><%= book.Price %></price>
									 <isbn><%= book.Isbn %></isbn>
									 <notes><%= book.Notes %></notes>
									 <summary><%= book.Summary %></summary>
									 <reviews>
										 <%= From review In book.Reviews _
											 Order By review.Rating _
											 Select <review>
																<user><%= review.UserEntity.Name %></user>
																<rating><%= review.Rating %></rating>
																<comments><%= review.Comments %></comments>
															</review>  %>
									 </reviews>
								 </book> %>
				</books>

Console.WriteLine(xml.ToString())