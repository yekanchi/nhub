<Query Kind="VBStatements" />

Dim count = _
  "Non-letter characters in this string: 8" _
	.Where(Function(c) Not Char.IsLetter(c)) _
	.Count()
count.Dump()