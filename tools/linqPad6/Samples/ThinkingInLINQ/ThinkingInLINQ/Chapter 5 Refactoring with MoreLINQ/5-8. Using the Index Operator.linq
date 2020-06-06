<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

string input = "LINQ";
List<KeyValuePair<int,char>> indices = new
List<KeyValuePair<int,char>>();
for(int i = 0; i< input.Length; i++)
{
	indices.Add(new KeyValuePair<int,char>(i,input[i]));
}
indices.Dump("Indices using loop");

"LINQ".ToCharArray().Index().Dump("Indices using MoreLINQ");