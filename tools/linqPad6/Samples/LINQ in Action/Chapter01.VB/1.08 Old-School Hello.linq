<Query Kind="VBStatements" />

Dim words As String() = {"hello", "wonderful", "linq", _
						 "beautiful", "world"}

For Each word As String In words
  If (word.Length <= 5) Then
	Console.WriteLine(word)
  End If
Next