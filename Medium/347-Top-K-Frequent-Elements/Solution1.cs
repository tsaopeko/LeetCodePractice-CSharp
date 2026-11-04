public class Solution {
    public int[] TopKFrequent(int[] nums, int k) {
        Dictionary<int, int> frequency = new Dictionary<int, int>();
        for(int i = 0; i < nums.Length; i++)
        {
            if(frequency.ContainsKey(nums[i]))
            {
                frequency[nums[i]]++;
            }
            else
            {
                frequency.Add(nums[i], 1);
            }
        }
        List<KeyValuePair<int, int>> list = frequency.ToList();
        list = list.OrderByDescending(x => x.Value).ToList();
        int[] result = list.Take(k).Select(x => x.Key).ToArray();
        return result;
    }
}