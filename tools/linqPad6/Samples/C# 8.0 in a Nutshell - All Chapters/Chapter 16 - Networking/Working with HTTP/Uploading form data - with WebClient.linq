<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

WebClient wc = new WebClient { Proxy = null };

var data = new System.Collections.Specialized.NameValueCollection();
data.Add ("Name", "Joe Albahari");
data.Add ("Company", "O'Reilly");

byte[] result = wc.UploadValues ("http://www.albahari.com/EchoPost.aspx",
                                 "POST", data);

Console.WriteLine (Encoding.UTF8.GetString (result));