<Query Kind="Statements" />

Regex.Match ("say 25 miles more", @"\d+\s(?=miles)").Value.Dump();
Regex.Match ("say 25 miles more", @"\d+\s(?=miles).*").Value.Dump();

string password = "blahblah3";
Regex.IsMatch (password, @"(?=.*\d).{6,}").Dump ("Password is strong");

password = "blahblaha";
Regex.IsMatch (password, @"(?=.*\d).{6,}").Dump ("Password is strong");

string regex = "(?i)good(?!.*(however|but))";
Regex.IsMatch ("Good work! But...",  regex).Dump ("Negative lookahead");
Regex.IsMatch ("Good work! Thanks!", regex).Dump ("Negative lookahead");

string fileNames = "a.txt" + "\r\n" + "b.docx" + "\r\n" + "c.txt";
string r = @".+\.txt(?=\r?$)";
foreach (Match m in Regex.Matches (fileNames, r, RegexOptions.Multiline))
	Console.Write (m + " ");