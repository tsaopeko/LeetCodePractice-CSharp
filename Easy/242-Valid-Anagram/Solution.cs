public class Solution {
    public bool IsAnagram(string s, string t) {
        // 1.檢查兩個字串長度是否一致
        if(s.Length != t.Length)
        {
            return false;
        }

        // 2.建立字典統計s中每個字母的數量
        Dictionary<char, int> charCounts = new Dictionary<char, int>();

        for(int i = 0; i < s.Length; i++)
        {
            if(charCounts.ContainsKey(s[i]))
            {
                charCounts[s[i]]++;
            }
            else
            {
                charCounts.Add(s[i], 1);
            }
        }

        // 3.讀取t逐一扣除對應字母的數量
        for(int i = 0; i < t.Length; i++)
        {
            if(charCounts.ContainsKey(t[i]))
            {
                charCounts[t[i]]--;
                if(charCounts[t[i]] < 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}