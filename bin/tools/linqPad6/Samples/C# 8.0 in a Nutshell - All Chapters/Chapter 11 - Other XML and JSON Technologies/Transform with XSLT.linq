<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Xml.Xsl</Namespace>
</Query>

#load ".\Create sample files.linq"

XslCompiledTransform transform = new XslCompiledTransform();
transform.Load ("customer.xslt");
transform.Transform ("customer.xml", "customer.xhtml");

File.ReadAllText("customer.xhtml").Dump();