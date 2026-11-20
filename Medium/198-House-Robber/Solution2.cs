public class Solution {
    public int Rob(int[] nums) {
        if(nums.Length == 0)
        {
            return 0;
        }

        if(nums.Length == 1)
        {
            return nums[0];
        }

        int prev = nums[0];
        int current = Math.Max(nums[0], nums[1]);

        for(int i = 2; i < nums.Length; i++)
        {
            int next = Math.Max(current, prev + nums[i]);
            prev = current;
            current = next;
        }
        return current;
    }
}