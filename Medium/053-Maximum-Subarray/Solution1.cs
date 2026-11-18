public class Solution {
    public int MaxSubArray(int[] nums) {
        int maxSum = int.MinValue;
        for(int i = 0; i < nums.Length; i++)
        {
            int currentSum = 0;
            for(int j = i; j < nums.Length; j++)
            {
                currentSum += nums[j];
                maxSum = Math.Max(maxSum, currentSum);
            }
        }
        return maxSum;
    }
}