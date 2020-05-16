<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Collections.Immutable</Namespace>
</Query>

void Main()
{
	var team = new Team ("Team", new Player ("Joe"));

	IFormatter formatter = new BinaryFormatter();

	using (FileStream s = File.Create ("serialized.bin"))
		formatter.Serialize (s, team);

	using (FileStream s = File.OpenRead ("serialized.bin"))
	{
		var team2 = (Team)formatter.Deserialize (s);
		team2.Dump();
	}
}

[Serializable]
public class Player
{
	public readonly string Name;
	public Player (string name) => Name = name;
}

[Serializable]
public class Team : ISerializable
{
	public readonly string Name;
	public readonly ImmutableList<Player> Players;

	public Team (string name, params Player[] players) 
	{
		Name = name;
		Players = players.ToImmutableList();
	}

	public virtual void GetObjectData (SerializationInfo si,
                                       StreamingContext sc)
	{
		si.AddValue ("Name", Name);
		si.AddValue ("PlayerData", Players.ToArray());
	}

	protected Team (SerializationInfo si, StreamingContext sc)
	{
		Name = si.GetString ("Name");

		// Deserialize Players to an array to match our serialization:
		Player[] p = (Player[])si.GetValue ("PlayerData", typeof (Player[]));

		// Construct a new List using this array:
		Players = p.ToImmutableList();
	}
}