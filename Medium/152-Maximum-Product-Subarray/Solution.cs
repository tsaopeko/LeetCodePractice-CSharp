public class Solution {
    public int MaxProduct(int[] nums) {
        int maxProduct = nums[0];
        int minProduct = nums[0];
        int result = nums[0];
        for(int i = 1; i < nums.Length; i++)
        {
            int newMax = Math.Max(nums[i], Math.Max(maxProduct * nums[i], minProduct * nums[i]));
            int newMin = Math.Min(nums[i], Math.Min(maxProduct * nums[i], minProduct * nums[i]));

            maxProduct = newMax;
            minProduct = newMin;

            result = Math.Max(result, maxProduct);
        }
        return result;
    }
}