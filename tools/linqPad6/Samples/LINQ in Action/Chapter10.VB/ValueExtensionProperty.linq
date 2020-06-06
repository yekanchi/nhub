<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = <books>
						  <book>LINQ in Action</book>
						  <book>Art of Unit Testing</book>
						</books>

Console.WriteLine(books.<book>.Value) ' Outputs LINQ in Action
