<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	var jax = new GameCharacter()
	{
		Role = GameRole.Figher,
		Level = 3,
		Rested = true
	};

	RollDamageIncludingWellRested (jax).Dump();
	RollDamageIncludingWellRested (jax).Dump();
	RollDamageIncludingWellRested (jax).Dump();
}

Random rnd = new Random();

int RollDamage(GameCharacter ch) => 
	ch switch
	{
		{ Role: GameRole.Figher } => ch.Level * rnd.Next (10),
		{ Role: GameRole.Rogue } => ch.Level * rnd.Next (6),
		{ Role: GameRole.Mage } => ch.Level * rnd.Next (4),
		_ => throw new Exception("Unexpected condition.")
	};
	
int RollDamageIncludingWellRested (GameCharacter ch) =>
	ch switch
	{
		{ Role: GameRole.Figher, Rested: true } => ch.Level * rnd.Next (12),
		{ Role: GameRole.Figher, Rested: false } => ch.Level * rnd.Next (10),
		{ Role: GameRole.Rogue, Rested: true } => ch.Level * rnd.Next (7),
		{ Role: GameRole.Rogue, Rested: false } => ch.Level * rnd.Next (6),
		// Well-rested doesn't affect mages
		{ Role: GameRole.Mage } => ch.Level * rnd.Next (4), 
		_ => throw new Exception ("Unexpected condition.")
	};

enum GameRole { Figher, Rogue, Mage };
class GameCharacter
{
	public GameRole Role;
	public int Level;
	public bool Rested;
}