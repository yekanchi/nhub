<Query Kind="Statements">
  
</Query>

object[] things = {"Sam","Dave","Greg","Travis","Dan",2};
List<string> allStrings = new List<string>();
foreach (var v in things)
{
	string z = v as string;
	if(z!=null)
		allStrings.Add(z);
}
allStrings.Dump("From Loop");

object[] things2 = {"Sam","Dave","Greg","Travis","Dan",2};
things2.Select (t => t as string)
.Where (t => t != null )
.Cast<string>()
.Dump("From LINQ");
