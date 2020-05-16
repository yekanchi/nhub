<Query Kind="Statements">
  
</Query>

int[] nums = {14,21,24,51,131,1,11,54};
List<int> skipWhileDivisibleBy7 = new List<int>();
for(int i=0;i<nums.Length;i++)
{
	if(nums[i] % 7 == 0)
	{
		continue;
	}
	else
		skipWhileDivisibleBy7.Add(nums[i]);
}
skipWhileDivisibleBy7.Dump("From Loop");

int[] nums2 = {14,21,24,51,131,1,11,54};
List<int> skipWhileDivisibleBy7l =
nums2.SkipWhile (n => n % 7 == 0).ToList();

skipWhileDivisibleBy7l.Dump("From LINQ");

