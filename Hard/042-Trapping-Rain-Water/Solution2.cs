public class Solution {
    public int Trap(int[] height) {
        int left = 0;
        int right = height.Length - 1;

        int leftMax = 0;
        int rightMax = 0;
        int totalWater = 0;

        while(left < right)
        {
            if(height[left] < height[right])
            {
                leftMax = Math.Max(leftMax, height[left]);
                totalWater += leftMax - height[left];
                left++;
            }
            else
            {
                rightMax = Math.Max(rightMax, height[right]);
                totalWater += rightMax - height[right];
                right--;
            }
        }

        return totalWater;
    }
}