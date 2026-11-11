public class Solution {
    public IList<IList<int>> ThreeSum(int[] nums) {
        List<IList<int>> result = new List<IList<int>>();
        Array.Sort(nums);
        for(int i = 0; i < nums.Length - 2; i++)
        {
            if(i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            if(nums[i] > 0)
            {
                break;
            }
            
            int j = i + 1;
            int k = nums.Length - 1;
            while(j < k)
            {
                int sum = nums[i] + nums[j] + nums[k];
                if(sum < 0)
                {
                    j++;
                }
                else if(sum > 0)
                {
                    k--;
                }
                else
                {
                    result.Add(new List<int> {nums[i], nums[j], nums[k]});

                    j++;
                    while(j < k && nums[j] == nums[j - 1])
                    {
                        j++;
                    }

                    k--;
                    while(j < k && nums[k] == nums[k + 1])
                    {
                        k--;
                    }
                }
            }
        }
        return result;
    }
}