public class Solution {
    /// <summary>
    /// 計算陣列中每個元素以外的所有元素乘積。
    /// </summary>
    /// <param name="nums">輸入的整數陣列。</param>
    /// <returns>每個位置排除自身後的乘積陣列。</returns>
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] result = new int[nums.Length];
        result[0] = 1;

        int rightProduct = 1;

        // 第一輪：計算左側乘積
        for (int i = 1; i < nums.Length; i++)
        {
            result[i] = result[i - 1] * nums[i - 1];
        }

        // 第二輪：乘上右側乘積
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            result[i] = result[i] * rightProduct;
            rightProduct *= nums[i];
        }

        return result;
    }
}