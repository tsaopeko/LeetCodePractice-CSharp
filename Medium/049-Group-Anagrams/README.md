# LeetCode 49 - Group Anagrams

**Difficulty：Medium**

## 1. 題目說明

給定一個字串陣列 `strs`，請將所有互為 Anagram（字母異位詞）的字串分組。

Anagram 指的是兩個字串包含相同的字母，而且每個字母出現的次數也相同，只是排列順序不同。

### 範例

**Input**
```csharp
strs = ["eat", "tea", "tan", "ate", "nat", "bat"]
```

**Output**
```csharp
[
    ["eat", "tea", "ate"],
    ["tan", "nat"],
    ["bat"]
]
```

## 2. 解題思考

這題的核心在於：

**如何判斷兩個字串是否具有相同的字母組成？**

例如：

- `eat`
- `tea`
- `ate`

雖然字母排列順序不同，但都包含 `a`、`e`、`t`，因此可以歸類在同一組。

思考後發現，可以利用 Dictionary 將相同特徵的字串放在同一個群組中。

Dictionary 的設計：

```csharp
Dictionary<string, List<string>> groups =
    new Dictionary<string, List<string>>();
```

- Key：用來識別字串的特徵。
- Value：儲存具有相同特徵的字串。

因此，解題的關鍵就是如何產生 Key。

本題使用兩種方法：

1. 排序法
2. 字母出現次數法

---

## 3. 解法一：排序法

### 解題思路

如果兩個字串是 Anagram，將它們的字母排序後，就會得到相同的字串。

例如：

| 原始字串 | 排序後 |
|---|---|
| eat | aet |
| tea | aet |
| ate | aet |
| tan | ant |
| nat | ant |
| bat | abt |

因此，可以將排序後的字串作為 Dictionary 的 Key。

### 程式碼

```csharp
using System;
using System.Collections.Generic;

public class Solution
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, List<string>> groups =
            new Dictionary<string, List<string>>();

        for (int i = 0; i < strs.Length; i++)
        {
            string word = strs[i];

            char[] chars = word.ToCharArray();
            Array.Sort(chars);

            string key = new string(chars);

            if (groups.ContainsKey(key))
            {
                groups[key].Add(word);
            }
            else
            {
                groups.Add(key, new List<string> { word });
            }
        }

        List<IList<string>> result = new List<IList<string>>();

        foreach (var group in groups)
        {
            result.Add(group.Value);
        }

        return result;
    }
}
```

### 程式碼解析

**1. 將字串轉換成字元陣列**

```csharp
char[] chars = word.ToCharArray();
```

因為需要對字元進行排序，所以先將字串轉換成字元陣列。

**2. 對字元陣列排序**

```csharp
Array.Sort(chars);
```

讓相同字母組成的字串產生相同的排序結果。

**3. 將排序後的字元陣列轉回字串**

```csharp
string key = new string(chars);
```

將排序後的字元陣列轉成字串，作為 Dictionary 的 Key。

**4. 判斷 Dictionary 是否已經存在該 Key**

```csharp
if (groups.ContainsKey(key))
{
    groups[key].Add(word);
}
else
{
    groups.Add(key, new List<string> { word });
}
```

- 如果 Key 已存在，就將目前字串加入原本的 List。
- 如果 Key 不存在，就建立新的群組。

**5. 整理結果**

```csharp
List<IList<string>> result = new List<IList<string>>();

foreach (var group in groups)
{
    result.Add(group.Value);
}
```

將 Dictionary 中每個群組的 Value 加入結果。

### 時間複雜度

假設：

- `n`：字串總數。
- `k`：每個字串的平均長度。

單一字串：

| 操作 | 時間複雜度 |
|---|---|
| ToCharArray() | O(k) |
| Array.Sort() | O(k log k) |
| new string() | O(k) |
| Dictionary 查找與加入 | 平均 O(k)（包含 Key 的雜湊計算） |

主要成本來自排序，因此總時間複雜度為：

**O(n × k log k)**

### 空間複雜度

需要儲存 Dictionary 的 Key、Value，以及最終分組結果，整體空間複雜度為：

**O(n × k)**

---

## 4. 解法二：字母出現次數法

### 解題思路

排序法需要對每個字串進行排序。

進一步思考：

如果兩個字串是 Anagram，它們不只排序後相同，而且每個字母的出現次數也完全相同。

例如：

`eat` 和 `tea`：

| 字母 | eat | tea |
|---|---:|---:|
| a | 1 | 1 |
| b | 0 | 0 |
| c | 0 | 0 |
| d | 0 | 0 |
| e | 1 | 1 |
| ... | ... | ... |
| t | 1 | 1 |

因此，可以利用陣列紀錄每個字母出現的次數。

題目限制字串只包含小寫英文字母 `a` 到 `z`，總共 26 個字母。

所以建立：

```csharp
int[] counts = new int[26];
```

### 如何取得陣列索引？

利用字元的 ASCII 數值：

```csharp
int index = word[j] - 'a';
```

例如：

| 字元 | ASCII 數值 | 索引 |
|---|---:|---:|
| a | 97 | 0 |
| b | 98 | 1 |
| c | 99 | 2 |
| z | 122 | 25 |

因此，每個字母都能對應到陣列中的位置。

### 程式碼

```csharp
using System;
using System.Collections.Generic;

public class Solution
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, List<string>> groups =
            new Dictionary<string, List<string>>();

        for (int i = 0; i < strs.Length; i++)
        {
            int[] counts = new int[26];

            string word = strs[i];

            for (int j = 0; j < word.Length; j++)
            {
                int index = word[j] - 'a';
                counts[index]++;
            }

            string key = string.Join(",", counts);

            if (groups.ContainsKey(key))
            {
                groups[key].Add(word);
            }
            else
            {
                groups.Add(key, new List<string> { word });
            }
        }

        List<IList<string>> result = new List<IList<string>>();

        foreach (var group in groups)
        {
            result.Add(group.Value);
        }

        return result;
    }
}
```

### 程式碼解析

**1. 建立字母次數陣列**

```csharp
int[] counts = new int[26];
```

每處理一個新字串，都必須重新建立陣列。

原因是不能讓前一個字串的統計結果累加到下一個字串。

**2. 計算字母出現次數**

```csharp
for (int j = 0; j < word.Length; j++)
{
    int index = word[j] - 'a';
    counts[index]++;
}
```

逐一讀取字串中的字元，將對應位置的計數加一。

**3. 將陣列轉換成 Dictionary Key**

```csharp
string key = string.Join(",", counts);
```

利用 `string.Join()` 將陣列中的數字轉換成字串，並使用逗號分隔。

為什麼需要逗號？

例如：

```text
[1, 11, 2]  →  "1,11,2"
[11, 1, 2]  →  "11,1,2"
```

如果沒有分隔符號，兩組數字可能產生相同的字串，因此需要使用逗號避免歧義。

另外，不能直接使用：

```csharp
counts.ToString();
```

因為這不會將陣列中的每個元素轉換成字串，而是回傳陣列的型別名稱。

**4. 使用 Dictionary 分組**

```csharp
if (groups.ContainsKey(key))
{
    groups[key].Add(word);
}
else
{
    groups.Add(key, new List<string> { word });
}
```

這部分與排序法完全相同。

兩種解法真正不同的地方只有 Key 的產生方式。

### 時間複雜度

假設：

- `n`：字串總數。
- `k`：每個字串的平均長度。

單一字串：

| 操作 | 時間複雜度 |
|---|---|
| 建立長度 26 的陣列 | O(1) |
| 遍歷字串計算次數 | O(k) |
| string.Join() | O(26) = O(1) |
| Dictionary 查找與加入 | 平均 O(1)，不含 Key 雜湊成本 |

因此單一字串的主要成本為 O(k)。

總時間複雜度：

**O(n × k)**

### 空間複雜度

Dictionary 需要儲存所有群組的 Key 和字串，整體空間複雜度為：

**O(n × k)**

每次使用的 `counts` 陣列只有 26 個元素，屬於固定大小的額外空間 O(1)。

---

## 5. 兩種解法比較

| 比較項目 | 排序法 | 字母次數法 |
|---|---|---|
| 核心概念 | 排序後產生 Key | 統計字母出現次數 |
| Key 內容 | 排序後的字串 | 26 個字母的次數 |
| 時間複雜度 | O(n × k log k) | O(n × k) |
| 空間複雜度 | O(n × k) | O(n × k) |
| 優點 | 思路直觀、容易理解 | 不需要排序，效率較高 |
| 限制 | 排序需要額外成本 | 直接適用於小寫英文字母限制 |

## 6. 本題學習重點

透過這題練習，理解了以下 C# 概念：

1. `Dictionary<TKey, TValue>`：利用 Key 將相同特徵的資料分組。
2. `List<string>`：在同一個 Key 底下儲存多個字串。
3. `ContainsKey()`：判斷 Dictionary 是否已經存在指定的 Key。
4. `ToCharArray()`：將字串轉換成字元陣列。
5. `Array.Sort()`：對字元陣列進行排序。
6. `new string(chars)`：將字元陣列轉回字串。
7. `string.Join()`：將陣列元素組合成字串。
8. ASCII 數值：利用 `word[j] - 'a'` 將小寫英文字母轉換成 0 到 25 的索引。
9. `IList<string>` 與 `List<string>`：理解 List 可以作為 IList 使用。