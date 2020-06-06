<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
</Query>

var client = new HttpClient();
var task1 = client.GetStringAsync ("http://www.linqpad.net");
var task2 = client.GetStringAsync ("http://www.albahari.com");

(await task1).Length.Dump ("First page length");
(await task2).Length.Dump ("Second page length");
