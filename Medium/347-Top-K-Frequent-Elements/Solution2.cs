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
        List<int>[] buckets = new List<int>[nums.Length + 1];
        foreach(var item in frequency)
        {
            if(buckets[item.Value] == null)
            {
                buckets[item.Value] = new List<int>();
            }
            buckets[item.Value].Add(item.Key);
        }
        List<int> result = new List<int>();
        for(int i = buckets.Length - 1; i >= 0; i--)
        {
            if(buckets[i] != null)
            {
                foreach(int num in buckets[i])
                {
                    result.Add(num);

                    if(result.Count == k)
                    {
                        break;
                    }
                } 
            }
            if(result.Count == k)
            {
                break;
            }
        }
        return result.ToArray();
    }
}