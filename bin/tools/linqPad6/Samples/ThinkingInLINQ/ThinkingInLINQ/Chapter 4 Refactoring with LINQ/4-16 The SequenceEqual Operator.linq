<Query Kind="Program">
  
</Query>

int[] codes = {343,2132,12,32143,234};
int[] expected = {343,12,2132,32143,234};
public bool IsSequenceEqual(int[] first,int[] second)
{
	if(first.Length == second.Length)
	{
		for(int i=0;i<first.Length;i++)
			if(first[i]!=second[i])
				return false;
		return true;
	}
	return false;
}
void Main()
{

	IsSequenceEqual(codes,expected).Dump("Sequence Equality Checked using Loop"); 
	codes.SequenceEqual(expected).Dump("Sequence Equality checked using LINQ");
}

