<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void DownloadPage()
{
	WebRequest req = WebRequest.Create
                    ("http://www.albahari.com/nutshell/code.html");
	req.Proxy = null;
	using (WebResponse res = req.GetResponse())
	using (Stream rs = res.GetResponseStream())
	using (FileStream fs = File.Create ("code_sync.html"))
		rs.CopyTo (fs);
}

async Task DownloadPageAsync()
{
	WebRequest req = WebRequest.Create
                    ("http://www.albahari.com/nutshell/code.html");
	req.Proxy = null;
	using (WebResponse res = await req.GetResponseAsync())
	using (Stream rs = res.GetResponseStream())
	using (FileStream fs = File.Create ("code_async.html"))
		await rs.CopyToAsync (fs);
}

DownloadPage();
await DownloadPageAsync();

foreach (var file in Directory.EnumerateFiles (".", "*.html"))
	Console.WriteLine ($"{file} {new FileInfo (file).Length} bytes");