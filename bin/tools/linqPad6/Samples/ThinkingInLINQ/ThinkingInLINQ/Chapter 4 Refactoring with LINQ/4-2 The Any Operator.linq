<Query Kind="Statements">
  
</Query>


//Loop (Imperative Paradigm)
int[] nums = {14,21,24,51,131,1,11,54};
bool isAny = false;
for(int i=0;i<nums.Length;i++)
{
	if(nums[i]>=150)
	{
		isAny = true;
		break;
	}
}

//LINQ (Functional Paradigm)
int[] nums2 = {14,21,24,51,131,1,11,54};
bool isAny2 = nums2.Any (n => n >= 150);

isAny.Dump();
isAny2.Dump();


