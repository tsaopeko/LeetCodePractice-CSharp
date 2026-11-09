public class Solution {
    public int LongestConsecutive(int[] nums) {
        HashSet<int> set = new HashSet<int>(nums);
        int maxLength = 0;
        foreach(int num in set)
        {
            if(set.Contains(num - 1) == false)
            {
                int current = num;
                int currentLength = 1;
                while (set.Contains(current + 1))
                {
                    current++;
                    currentLength++;
                }
                
                maxLength = Math.Max(maxLength, currentLength);
            }
        }
        return maxLength;
    }
}