<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

int cardNumber = 12; string suite = "spades";

string cardRank = cardNumber switch
{
	13 => "King",
	12 => "Queen",
	11 => "Jack",
	_ => "Pip card"   // equivalent to 'default'
};

cardRank.Dump();

string cardName = (cardNumber, suite) switch
{
	(13, "spades") => "King of spades",
	(13, "clubs") => "King of clubs",
	(13, "hearts") => "King of hearts",
	(13, "diamonds") => "King of diamonds",
	(12, "spades") => "Queen of spades",
	(12, "clubs") => "Queen of clubs",
	(12, "hearts") => "Queen of hearts",
	(12, "diamonds") => "Queen of diamonds",
	_ => "Something else" // The discard is also used for a tuple
};

cardName.Dump();