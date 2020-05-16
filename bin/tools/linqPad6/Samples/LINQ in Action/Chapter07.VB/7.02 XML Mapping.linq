<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>LinqInAction.Chapter06to08.Common.SampleClasses.Ch7</Namespace>
</Query>

Sub Main
	Dim map as XmlMappingSource = _
		XmlMappingSource.FromXml(XmlMapping())
		
	Dim dataContext As New DataContext(Connection.ConnectionString, map)
	dataContext.Log = Console.Out
	
	Dim authors = dataContext.GetTable(Of AuthorXml)
	authors.Dump()
	
End Sub

Function XmlMapping()
	'Typically you would save this in a file, but due to the LINQPad
	'integration, we'll just hardcode this XML here. 
	Return <?xml version="1.0" encoding="utf-16"?>
<Database Name="lia" xmlns="http://schemas.microsoft.com/linqtosql/mapping/2007">
  <Table Name="Author">
	<Type Name="LinqInAction.Chapter06to08.Common.SampleClasses.Ch7.AuthorXml">
	  <Column Name="ID" Member="ID" Storage="_Id" DbType="UniqueIdentifier NOT NULL" IsPrimaryKey="True" />
	  <Column Name="LastName" Member="LastName" DbType="VarChar(50) NOT NULL" CanBeNull="False" />
	  <Column Name="FirstName" Member="FirstName" DbType="VarChar(30)" />
	  <Column Name="WebSite" Member="WebSite" DbType="VarChar(200)" />
	  <Column Name="TimeStamp" Member="TimeStamp" DbType="rowversion NOT NULL" CanBeNull="False" IsDbGenerated="True" IsVersion="True" AutoSync="Always" />
	</Type>
  </Table>
</Database>.ToString()
End Function