<Query Kind="Program">
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

void Main()
{
	var slow = GetSomeTokens().ToObservable();
	var fast = GetSomeTokensFast().ToObservable();
	Observable.Merge(slow,fast).Dump();
}
public IEnumerable<string> GetSomeTokensFast()
{
	string[] names = {"A","B","C","D","E","F","G"};
	for(int i = 0;i<names.Length;i++)
	{
		Thread.Sleep(new Random().Next(500));
		yield return names[i];
	}
}
public IEnumerable<string> GetSomeTokens()
{
	string[] names = {"Af","fB","fD","fE","fF","fG"};
	for(int i = 0;i<names.Length;i++)
	{
		Thread.Sleep(new Random().Next(1000));
		yield return names[i];
	}
}