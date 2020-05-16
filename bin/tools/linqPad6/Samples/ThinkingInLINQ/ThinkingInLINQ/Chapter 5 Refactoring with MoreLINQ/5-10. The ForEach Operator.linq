<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] numbers = {1,2,8,7,5,6,4,3};
Action<int> ack = a => Console.WriteLine(DateTime.Today.AddDays(a).DayOfWeek);
foreach (var integer in numbers)
	ack.Invoke(integer);

Console.WriteLine("****************");
Action<int> ack2 = a => Console.WriteLine(DateTime.Today.AddDays(a).DayOfWeek);
numbers.ForEach(ack2);