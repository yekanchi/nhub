<Query Kind="Statements">
  
</Query>

int[] nums = {14,21,24,51,131,1,11,54};
int[] above50 = new int[nums.Length];
int j = 0;
for(int i=0;i<nums.Length;i++)
{
	if(nums[i] > 50)
	{
		above50[j]=nums[i];
		j++;
	}
}
Array.Resize(ref above50,j);

above50.Dump("From Loop");

int[] nums2 = {14,21,24,51,131,1,11,54}; int[]
above50l = nums2.Where (n => n > 50).ToArray();
above50l.Dump("From LINQ");
