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

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim xml As XElement = <books>
						<%= From line In System.IO.File.ReadAllLines(PAth & "books.txt.sdf") _
							Where Not line.StartsWith("#") _
							Let items = line.Split(",") _
							Select _
							<book>
								<title><%= items(1) %></title>
								<authors>
									<%= From authorFullName In items(2).Split(";") _
										Let authorNameParts = authorFullName.Split(" ") _
										Select <author>
														 <firstName><%= authorNameParts(0) %></firstName>
														 <lastName><%= authorNameParts(1) %></lastName>
													 </author> _
									%>
								</authors>
								<publisher><%= items(3) %></publisher>
								<publicationDate><%= items(4) %></publicationDate>
								<price><%= items(5) %></price>
								<isbn><%= items(0) %></isbn>
							</book> _
						%>
					</books>

Console.WriteLine(xml)