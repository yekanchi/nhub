<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	var json = @"
	{
		""Id"":27182,
		""Name"":""Sara"",
		""Born"":464572800
	}";
	
	var sara = JsonSerializer.Deserialize<Person> (json);  
	sara.Dump();
}

public class Person
{
	public int Id { get; set; }
	public string Name { get; set; }
	
	[JsonConverter(typeof(UnixTimestampConverter))]
	public DateTime Born { get; set; }
}

public class UnixTimestampConverter : JsonConverter<DateTime>
{
	public override DateTime Read (ref Utf8JsonReader reader, Type type,
                                   JsonSerializerOptions options)
	{
		if (reader.TryGetInt32(out int timestamp))
			return new DateTime (1970, 1, 1).AddSeconds (timestamp);

		throw new Exception ("Expected the timestamp as a number.");
	}

	public override void Write (Utf8JsonWriter writer, DateTime value,
                                JsonSerializerOptions options)
	{
		int timestamp = (int)(value - new DateTime(1970, 1, 1)).TotalSeconds;
		writer.WriteNumberValue(timestamp);
	}
}