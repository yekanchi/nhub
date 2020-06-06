<query Kind="Statements" />
string[] words = { "hello", "wonderful", "linq", "beautiful", "world" };

// Get only short words
var shortWords =
  from word in words
  where word.Length <= 5
  select word;

// Print each word out
shortWords.Dump();