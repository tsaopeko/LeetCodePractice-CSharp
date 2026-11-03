public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
        
        for(int i = 0; i < strs.Length; i++)
        {
            int[] counts = new int[26];
            string word = strs[i];
            for(int j = 0; j < word.Length; j++)
            {
                int index = word[j] - 'a';
                counts[index]++;
            }

            string key = string.Join(",", counts);

            if(groups.ContainsKey(key))
            {
                groups[key].Add(word);
            }
            else
            {
                groups.Add(key, new List<string> { word });
            }
        }
        
        List<IList<string>> result = new List<IList<string>>();

        foreach(var group in groups)
        {
            result.Add(group.Value);
        }

        return result;
    }
}