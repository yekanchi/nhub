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
		Level = 3
	};
	
	Console.WriteLine(GetTitle(jax));
}

enum GameRole { Figher, Rogue, Mage };
class GameCharacter
{
	public GameRole Role;
	public int Level;
	public void Deconstruct(out GameRole role, out int level) => 
		(role, level) = (Role, Level);
}

string GetTitle(GameCharacter ch) => ch switch
{
	var (role, level) when role == GameRole.Figher && level > 9 => "Knight",
	var (role, level) when role == GameRole.Figher && level > 2 => "Warrior",
	var (role, level) when role == GameRole.Figher => "Squire",
	var (role, level) when role == GameRole.Rogue && level > 9 => "Master",
	var (role, level) when role == GameRole.Rogue && level > 2 => "Footpad",
	var (role, level) when role == GameRole.Rogue => "Recuit",
	var (role, level) when role == GameRole.Mage && level > 9 => "Archmage",
	var (role, level) when role == GameRole.Mage && level > 2 => "Caster",
	var (role, level) when role == GameRole.Mage => "Apprentice",
	_ => throw new Exception("Unexpected values.")
};