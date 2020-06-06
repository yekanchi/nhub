<Query Kind="Statements" />

var count =
  "Non-letter characters in this string: 8"
    .Where(c => !Char.IsLetter(c))
    .Count();
count.Dump();