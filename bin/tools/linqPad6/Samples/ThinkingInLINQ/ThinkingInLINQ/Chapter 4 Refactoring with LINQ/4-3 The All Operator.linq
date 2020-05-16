<Query Kind="Statements">
  
</Query>

int[] nums = {14,21,24,51,131,1,11,54};
bool isAll = true;
for(int i=0;i<nums.Length;i++)
{
	if(nums[i]<150)
	{
		isAll = false;
		break;
	}
}

int[] nums2 = {14,21,24,51,131,1,11,54};
bool isAll2 = nums.All (n => n < 150);
