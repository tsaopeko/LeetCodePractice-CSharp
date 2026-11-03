public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
        for(int i = 0; i < strs.Length; i++)
        {
            string word = strs[i];

            char[] chars = word.ToCharArray();
            Array.Sort(chars);

            string key = new string(chars);
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