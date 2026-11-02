# LeetCode 242 - Valid Anagram

- **Difficulty:** Easy
- **Topic:** String、Hash Table、Dictionary
- **LeetCode:** [242. Valid Anagram](https://leetcode.com/problems/valid-anagram/)

## 一、題目說明

給定兩個字串 `s` 和 `t`，請判斷 `t` 是否為 `s` 的字母異位詞（Anagram）。

字母異位詞是指：兩個字串包含的字母完全相同，而且每個字母出現的次數也相同，只是字母的排列順序可以不同。

### 範例一

```text
Input: s = "anagram", t = "nagaram"
Output: true
```

說明：

兩個字串都包含相同數量的 `a`、`n`、`g`、`r`、`m`，因此回傳 `true`。

### 範例二

```text
Input: s = "rat", t = "car"
Output: false
```

說明：

`s` 包含 `r`、`a`、`t`，而 `t` 包含 `c`、`a`、`r`。

兩個字串的字母組成不同，因此回傳 `false`。

## 二、解題思路

本題使用 `Dictionary<char, int>` 統計字串中每個字母出現的次數。

字典的 Key 是字母，Value 是該字母出現的次數。

例如：

```csharp
string s = "aab";
```

統計結果：

| Key | Value |
|---|---:|
| `a` | 2 |
| `b` | 1 |

接著讀取第二個字串 `t`，每遇到一個字母，就將字典中對應的數量減少 `1`。

如果字母不存在，或減少後的數量小於 `0`，代表兩個字串的字母組成不一致，可以直接回傳 `false`。

如果所有字母都能成功配對，最後就回傳 `true`。

### 解題步驟

1. 比較兩個字串的長度。如果長度不同，直接回傳 `false`。
2. 建立 `Dictionary<char, int>`。
3. 使用第一個 `for` 迴圈統計 `s` 中每個字母的數量。
4. 使用第二個 `for` 迴圈逐一檢查 `t` 中的字母。
5. 如果字母不存在於字典，回傳 `false`。
6. 將對應字母的數量減少 `1`。如果數量小於 `0`，回傳 `false`。
7. 如果整個迴圈都沒有發現問題，回傳 `true`。

## 三、完整程式碼

```csharp
using System.Collections.Generic;

public class Solution
{
    public bool IsAnagram(string s, string t)
    {
        // 1. 檢查兩個字串長度是否一致
        if (s.Length != t.Length)
        {
            return false;
        }

        // 2. 建立字典，統計 s 中每個字母的數量
        Dictionary<char, int> charCounts = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            if (charCounts.ContainsKey(s[i]))
            {
                charCounts[s[i]]++;
            }
            else
            {
                charCounts.Add(s[i], 1);
            }
        }

        // 3. 讀取 t，逐一扣除對應字母的數量
        for (int i = 0; i < t.Length; i++)
        {
            if (charCounts.ContainsKey(t[i]))
            {
                charCounts[t[i]]--;

                if (charCounts[t[i]] < 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // 4. 所有字母都成功配對
        return true;
    }
}
```

## 四、程式碼解析

### 1. 檢查字串長度

```csharp
if (s.Length != t.Length)
{
    return false;
}
```

兩個字串的長度不同，就不可能包含完全相同數量的字母。

因此，可以先進行長度檢查，避免不必要的計算。

### 2. 建立 Dictionary

```csharp
Dictionary<char, int> charCounts = new Dictionary<char, int>();
```

- `char`：儲存單一字元，例如 `'a'`。
- `int`：儲存字元出現的次數。
- `charCounts`：記錄 `s` 中每個字母的數量。

### 3. 統計第一個字串

```csharp
for (int i = 0; i < s.Length; i++)
{
    if (charCounts.ContainsKey(s[i]))
    {
        charCounts[s[i]]++;
    }
    else
    {
        charCounts.Add(s[i], 1);
    }
}
```

使用 `ContainsKey()` 判斷字典是否已經存在該字母。

- 如果存在，就將數量加 `1`。
- 如果不存在，就新增該字母，並將數量設為 `1`。

例如 `s = "aab"`：

| 讀取字元 | 執行動作 | 字典內容 |
|---|---|---|
| `a` | 新增 `a = 1` | `a: 1` |
| `a` | 將 `a` 加 `1` | `a: 2` |
| `b` | 新增 `b = 1` | `a: 2, b: 1` |

最後，字典會記錄：

```text
a = 2
b = 1
```

### 4. 檢查第二個字串

```csharp
for (int i = 0; i < t.Length; i++)
{
    if (charCounts.ContainsKey(t[i]))
    {
        charCounts[t[i]]--;

        if (charCounts[t[i]] < 0)
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}
```

這段程式碼會逐一檢查 `t` 中的字母。

首先，使用 `ContainsKey()` 確認字典是否包含該字母。

如果不存在，代表 `t` 出現了 `s` 沒有的字母，直接回傳 `false`。

如果存在，就將字典中該字母的數量減少 `1`。

如果數量變成負數，代表 `t` 使用該字母的次數超過 `s`，同樣回傳 `false`。

### 5. 回傳結果

```csharp
return true;
```

如果兩個迴圈都順利完成，代表 `t` 中的所有字母都能與 `s` 配對，因此回傳 `true`。

## 五、執行過程分析

### 範例一：回傳 true

```csharp
string s = "aab";
string t = "aba";
```

先統計 `s`：

| 字母 | 數量 |
|---|---:|
| `a` | 2 |
| `b` | 1 |

接著逐一讀取 `t`：

| 步驟 | 讀取字元 | `a` 剩餘數量 | `b` 剩餘數量 |
|---|---|---:|---:|
| 初始 | — | 2 | 1 |
| 1 | `a` | 1 | 1 |
| 2 | `b` | 1 | 0 |
| 3 | `a` | 0 | 0 |

所有字母都成功配對，沒有任何數量小於 `0`，因此回傳 `true`。

### 範例二：回傳 false

```csharp
string s = "aab";
string t = "abb";
```

先統計 `s`：

| 字母 | 數量 |
|---|---:|
| `a` | 2 |
| `b` | 1 |

接著讀取 `t`：

| 步驟 | 讀取字元 | `a` 剩餘數量 | `b` 剩餘數量 |
|---|---|---:|---:|
| 初始 | — | 2 | 1 |
| 1 | `a` | 1 | 1 |
| 2 | `b` | 1 | 0 |
| 3 | `b` | 1 | -1 |

第三次讀取時，`b` 的數量變成 `-1`。

這表示 `t` 中的 `b` 比 `s` 多，因此回傳 `false`。

## 六、時間複雜度

假設兩個字串的長度為 `n`。

| 操作 | 時間複雜度 |
|---|---|
| 比較字串長度 | O(1) |
| 統計 `s` 的字母數量 | O(n) |
| 檢查 `t` 的字母數量 | O(n) |
| **總時間複雜度** | **O(n)** |

雖然程式使用兩個 `for` 迴圈，但它們是依序執行，而不是巢狀迴圈。

因此，整體時間複雜度是 O(n)，不是 O(n²)。

## 七、空間複雜度

使用 `Dictionary<char, int>` 儲存不同字母及其數量。

假設字串中有 `k` 種不同字元，字典最多需要儲存 `k` 筆資料。

因此，空間複雜度為：

**O(k)**

如果題目限制字串只包含小寫英文字母，最多只有 26 種字母，因此字典的大小有固定上限，空間複雜度可以視為 **O(1)**。

## 八、解題心得

這題讓我練習使用 `Dictionary<char, int>` 統計字串中的字母數量，並利用數量的增減來判斷兩個字串是否為字母異位詞。

### 學習重點

1. 使用 `Dictionary` 統計資料出現的次數。
2. 使用 `ContainsKey()` 檢查 Key 是否存在。
3. 使用 `++` 與 `--` 更新字典中的數值。
4. 利用數量是否小於 `0` 判斷字母是否超額使用。
5. 使用 `return false` 提早結束不符合條件的情況。
6. 理解兩個依序執行的迴圈，其時間複雜度仍然是 O(n)。

### 延伸思考

這題與之前練習的 LeetCode 217 - Contains Duplicate 都使用了雜湊相關的資料結構，但解決的問題不同：

- **Contains Duplicate**：使用 `HashSet` 判斷是否有重複元素。
- **Valid Anagram**：使用 `Dictionary` 統計每個字母的數量。

兩題都能透過選擇適合的資料結構，避免使用效率較低的巢狀迴圈。