public class Solution {
    public int Trap(int[] height) {
        int[] leftMax = new int[height.Length];
        int[] rightMax = new int[height.Length];
        int totalWater = 0;
        leftMax[0] = height[0];
        rightMax[height.Length - 1] = height[height.Length - 1];

        if(height == null || height.Length < 3)
        {
            return 0;
        }
        
        for(int i = 1; i < height.Length; i++)
        {
            leftMax[i] = Math.Max(leftMax[i - 1], height[i]);
        }

        for(int i = height.Length - 2; i >= 0; i--)
        {
            rightMax[i] = Math.Max(rightMax[i + 1], height[i]);
        }

        for(int i = 0; i < height.Length; i++)
        {
            totalWater += Math.Min(leftMax[i], rightMax[i]) - height[i];
        }

        return totalWater;
    }
}