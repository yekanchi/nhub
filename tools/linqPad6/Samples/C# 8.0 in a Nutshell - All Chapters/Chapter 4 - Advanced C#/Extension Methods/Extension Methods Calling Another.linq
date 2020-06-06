<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	Console.WriteLine ("FF".IsHexNumber());  // True
	Console.WriteLine ("1A".NotHexNumber()); // False
}

static public class Ext
{
	static public bool IsHexNumber (this string candidate)
	{
		return int.TryParse(candidate, NumberStyles.HexNumber, null, out int _);
	}
	static public bool NotHexNumber (this string candidate)
	{
		return !IsHexNumber (candidate);
	}
}