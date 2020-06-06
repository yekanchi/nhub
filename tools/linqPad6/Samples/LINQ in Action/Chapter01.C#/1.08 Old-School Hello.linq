<query Kind="Statements" />
string[] words = new string[] { "hello", "wonderful", "linq",
								"beautiful", "world" };

foreach (string word in words)
{
  if (word.Length <= 5)
    Console.WriteLine(word);
}