<Query Kind="Statements">
  
</Query>

int[] nums = {14,21,24,51,131,1,11,54};
int[] skip4 = new
int[nums.Length - 4];
for(int i=4,j=0;i<nums.Length;i++,j++)
{
	skip4[j] = nums[i];
}
skip4.Dump("From Loop");

int[] nums2 = {14,21,24,51,131,1,11,54};
int[] skip4l = new int[nums.Length - 4];
skip4l = nums2.Skip(4).ToArray();
skip4l.Dump("From LINQ");
