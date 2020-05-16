<Query Kind="Program" />

void Main()
{
	var test1 = Run ("ipconfig.exe");
	test1.output.Dump ("Output");
	test1.errors.Dump ("Errors");
}

(string output, string errors) Run (string exePath, string args = "")
{
	using var p = Process.Start (new ProcessStartInfo (exePath, args)
	{
		RedirectStandardOutput = true,
		RedirectStandardError = true,
		UseShellExecute = false,
	});

	var errors = new StringBuilder ();

	// Read from the error stream asynchronously...
	p.ErrorDataReceived += (sender, errorArgs) =>
	{
		if (errorArgs.Data != null) errors.AppendLine (errorArgs.Data);
	};
	p.BeginErrorReadLine ();

	// ...while we read from the output stream synchronously:
	string output = p.StandardOutput.ReadToEnd();

	p.WaitForExit();
	return (output, errors.ToString());
}