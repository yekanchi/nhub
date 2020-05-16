<Query Kind="Program">
  
</Query>

public class StringLengthComparer :IComparer<string>
{
	public int Compare(string x, string y)
	{
		return x.Length.CompareTo(y.Length);
	}
}
void Main()
{
	string[] codes = {"abc","bc","a","d","abcd"};
	StringLengthComparer slc = new StringLengthComparer();
	List<string> codesAsList = codes.ToList();
	codesAsList.Sort(slc);
	codesAsList.Dump("From Loop");
	
	codes.OrderBy (item  => item.Length).Dump("From LINQ");
}
