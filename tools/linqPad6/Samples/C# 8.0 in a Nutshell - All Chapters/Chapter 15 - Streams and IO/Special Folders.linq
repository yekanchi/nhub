<Query Kind="Statements" />

foreach (var val in Enum.GetValues(typeof(Environment.SpecialFolder)).Cast<Environment.SpecialFolder>().Distinct().OrderBy(v =>v.ToString()))
{
	$"{Environment.GetFolderPath (val)}".Dump(val.ToString());
}