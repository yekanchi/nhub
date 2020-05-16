<Query Kind="Statements">
  
</Query>

int[] nums = {14,21,24,51,131,1,11,54};
List<int> until50 = new
List<int>();
for(int i=0;i<nums.Length;i++) 
{
	if(nums[i]<50)
	{
		until50.Add(nums[i]);
	}
	else
		break;
}
until50.Dump("From Loop");


int[] nums2 = {14,21,24,51,131,1,11,54};
List<int> until50l = new List<int>();
until50l = nums2.TakeWhile (n => n < 50).ToList();
until50l.Dump("From LINQ");
