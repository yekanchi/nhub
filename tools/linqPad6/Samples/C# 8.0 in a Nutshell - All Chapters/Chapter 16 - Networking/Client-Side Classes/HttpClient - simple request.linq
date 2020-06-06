<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
</Query>

string html = await new HttpClient().GetStringAsync ("http://linqpad.net");
html.Dump();

