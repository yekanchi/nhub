<Query Kind="Statements" />

int cardNumber = 13;

string cardName = cardNumber switch
{
	13 => "King",
	12 => "Queen",
	11 => "Jack",
	_ => "Pip card"   // equivalent to 'default'
};

cardName.Dump();

string suite = "spades";
string cardName2 = (cardNumber, suite) switch   // tuple pattern
{
	(13, "spades") => "King of spades",
	(13, "clubs") => "King of clubs",
	_ => "Other"
};

cardName2.Dump();