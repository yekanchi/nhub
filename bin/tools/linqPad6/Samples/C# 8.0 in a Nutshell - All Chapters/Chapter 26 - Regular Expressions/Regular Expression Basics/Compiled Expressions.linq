<Query Kind="Statements" />

Regex r = new Regex (@"sausages?", RegexOptions.Compiled);

r.Match ("sausage").Success.Dump();
r.Match ("sausages").Success.Dump();