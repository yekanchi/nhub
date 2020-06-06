<Query Kind="Statements">
  
</Query>

int[] nums = {14,21,24,51,131,1,11,54};
int[] first4 = new int[4];
for(int i=0;i<4;i++)
{
	first4[i] = nums[i]; 
}
first4.Dump("First 4 from loop");

int[] nums2 = {14,21,24,51,131,1,11,54};
int[] first4l = new int[4];
first4l = nums2.Take(4).ToArray();

first4l.Dump("First 4 from LINQ");
