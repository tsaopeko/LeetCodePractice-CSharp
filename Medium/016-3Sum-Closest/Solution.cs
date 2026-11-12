public class Solution {
    public int ThreeSumClosest(int[] nums, int target) {
        Array.Sort(nums);
        int closestSum = nums[0] + nums[1] + nums[2];
        for(int i = 0; i < nums.Length - 2; i++)
        {
            int j = i + 1;
            int k = nums.Length - 1;
            while(j < k)
            {
                int sum = nums[i] + nums[j] + nums[k];
                if(Math.Abs(sum - target) < Math.Abs(closestSum - target))
                {
                    closestSum = sum;
                }

                if(sum < target)
                {
                    j++;
                }
                else if(sum > target)
                {
                    k--;
                }
                else
                {
                    return sum;
                }
            }
        }
        return closestSum;
    }
}