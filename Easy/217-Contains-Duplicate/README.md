# LeetCode 217 - Contains Duplicate

- **Difficulty:** Easy
- **Topics:** Array, Hash Table, HashSet
- **Problem Link:** [LeetCode 217 - Contains Duplicate](https://leetcode.com/problems/contains-duplicate/)

---

## 1. 題目描述（Problem Description）

給定一個整數陣列 `nums`，判斷陣列中是否存在重複的元素。

- 如果任何一個數字至少出現兩次，回傳 `true`。
- 如果每個數字都只出現一次，回傳 `false`。

### 範例一

```csharp
nums = new int[] { 1, 2, 3, 1 };
```

輸出：

```text
true
```

原因：數字 `1` 出現了兩次。

### 範例二

```csharp
nums = new int[] { 1, 2, 3, 4 };
```

輸出：

```text
false
```

原因：所有數字都只出現一次。

### 範例三

```csharp
nums = new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 };
```

輸出：

```text
true
```

原因：陣列中存在多個重複的數字。

---

## 2. 解題思路（Approach）

這題的核心是判斷：

**目前遇到的數字，是否曾經出現過。**

可以使用兩種方法解決。

### 方法一：暴力解法（Brute Force）

使用兩層 `for` 迴圈，讓每個數字與後面的數字逐一比較。

如果兩個數字相同，就代表找到了重複元素，直接回傳 `true`。

如果所有比較都完成，仍然沒有找到重複元素，就回傳 `false`。

### 方法二：使用 HashSet

使用 `HashSet<int>` 記錄已經出現過的數字。

每次讀取一個數字時：

1. 使用 `Contains()` 檢查數字是否已經存在。
2. 如果存在，代表出現重複元素，回傳 `true`。
3. 如果不存在，使用 `Add()` 將數字加入集合。
4. 繼續檢查下一個數字。

如果遍歷完整個陣列都沒有找到重複元素，就回傳 `false`。

---

## 3. 解法一：暴力解法（Brute Force）

### 程式碼

```csharp
public class Solution
{
    public bool ContainsDuplicate(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] == nums[j])
                {
                    return true;
                }
            }
        }

        return false;
    }
}
```

### 程式解析

#### 第一層迴圈

```csharp
for (int i = 0; i < nums.Length; i++)
```

第一層迴圈負責選擇目前要檢查的數字。

例如：

```text
nums = { 5, 8, 2, 8, 9 }
```

當 `i = 0` 時，取得的數字是 `5`。

當 `i = 1` 時，取得的數字是 `8`。

依此類推，直到檢查完整個陣列。

#### 第二層迴圈

```csharp
for (int j = i + 1; j < nums.Length; j++)
```

第二層迴圈負責將目前的數字與後面的數字進行比較。

使用 `i + 1` 作為起始位置，有兩個原因：

1. 不需要拿數字與自己比較。
2. 不需要重複比較已經檢查過的數字組合。

例如，當 `i = 1` 時，`j` 會從 `2` 開始。

因此，`nums[1]` 只會與 `nums[2]`、`nums[3]`、`nums[4]` 比較。

#### 判斷是否重複

```csharp
if (nums[i] == nums[j])
{
    return true;
}
```

如果兩個位置的數字相同，就代表找到了重複元素。

此時使用 `return true` 直接結束整個方法，不必再進行其他比較。

#### 回傳 false

```csharp
return false;
```

如果程式執行到這裡，表示所有必要的比較都已經完成，而且沒有找到重複元素。

因此回傳 `false`。

### 暴力解法的執行流程

假設：

```csharp
int[] nums = { 5, 8, 2, 8, 9 };
```

比較過程如下：

| i | j | nums[i] | nums[j] | 結果 |
|---:|---:|---:|---:|---|
| 0 | 1 | 5 | 8 | 不相同 |
| 0 | 2 | 5 | 2 | 不相同 |
| 0 | 3 | 5 | 8 | 不相同 |
| 0 | 4 | 5 | 9 | 不相同 |
| 1 | 2 | 8 | 2 | 不相同 |
| 1 | 3 | 8 | 8 | 相同，回傳 true |

當 `i = 1`、`j = 3` 時，程式找到重複元素，因此直接結束。

### 暴力解法的複雜度

**時間複雜度：O(n²)**

在沒有重複元素的情況下，程式需要比較所有不同位置的元素組合。

比較次數為：

```text
n × (n - 1) / 2
```

因此時間複雜度為 O(n²)。

**空間複雜度：O(1)**

除了輸入陣列以外，只使用固定數量的變數 `i` 和 `j`，沒有建立額外的集合。

因此額外空間複雜度為 O(1)。

---

## 4. 解法二：使用 HashSet

### 程式碼

```csharp
using System.Collections.Generic;

public class Solution
{
    public bool ContainsDuplicate(int[] nums)
    {
        HashSet<int> numbers = new HashSet<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (numbers.Contains(nums[i]))
            {
                return true;
            }

            numbers.Add(nums[i]);
        }

        return false;
    }
}
```

### 程式解析

#### 建立 HashSet

```csharp
HashSet<int> numbers = new HashSet<int>();
```

建立一個用來儲存整數的 HashSet。

HashSet 不會儲存重複的元素，因此很適合用來記錄哪些數字已經出現過。

一開始集合是空的：

```text
numbers = { }
```

#### 遍歷陣列

```csharp
for (int i = 0; i < nums.Length; i++)
```

使用一層 `for` 迴圈，依序讀取陣列中的每個數字。

與暴力解法不同，這裡不需要第二層迴圈。

#### 檢查數字是否存在

```csharp
if (numbers.Contains(nums[i]))
{
    return true;
}
```

`Contains()` 用來判斷指定的數字是否已經存在於 HashSet。

如果回傳 `true`，代表這個數字之前已經出現過。

因此可以直接回傳 `true`。

#### 記錄目前的數字

```csharp
numbers.Add(nums[i]);
```

如果程式沒有進入前面的 `if`，代表目前的數字尚未出現過。

因此使用 `Add()` 將它加入 HashSet，讓後續的數字可以與它進行查找。

這裡的執行順序非常重要：

**先使用 Contains() 檢查，再使用 Add() 記錄。**

#### 回傳 false

```csharp
return false;
```

如果遍歷完整個陣列，始終沒有找到重複元素，就回傳 `false`。

### HashSet 解法的執行流程

假設：

```csharp
int[] nums = { 5, 8, 2, 8, 9 };
```

執行過程如下：

| 目前數字 | 檢查前的 HashSet | 是否已存在 | 執行動作 |
|---:|---|---|---|
| 5 | 空集合 | 否 | 加入 5 |
| 8 | {5} | 否 | 加入 8 |
| 2 | {5, 8} | 否 | 加入 2 |
| 8 | {5, 8, 2} | 是 | 回傳 true |

當程式第二次讀取到 `8` 時，HashSet 已經包含 `8`。

因此，程式直接回傳 `true`，不需要繼續檢查 `9`。

### HashSet 解法的複雜度

**時間複雜度：平均 O(n)**

整個陣列最多遍歷一次。

HashSet 的 `Contains()` 與 `Add()` 平均都可以在 O(1) 時間內完成，因此整體平均時間複雜度為 O(n)。

需要注意的是，這是平均情況；雜湊操作在最壞情況下可能需要 O(n) 時間。

**空間複雜度：O(n)**

如果所有數字都不重複，HashSet 最多需要儲存 n 個元素。

因此額外空間複雜度為 O(n)。

---

## 5. 兩種解法的比較

| 比較項目 | 暴力解法 | HashSet 解法 |
|---|---|---|
| 使用的迴圈 | 雙層 for | 單層 for |
| 查找方式 | 逐一比較數字 | 查詢集合中是否存在 |
| 平均時間複雜度 | O(n²) | O(n) |
| 額外空間複雜度 | O(1) | O(n) |
| 主要優點 | 寫法直觀，不需額外集合 | 查找效率高 |
| 適合學習的觀念 | 巢狀迴圈與元素比較 | HashSet 與雜湊查找 |

HashSet 解法使用額外空間儲存已經出現的數字，換取更好的平均執行效率。

這就是以空間換取時間的概念。

---

## 6. 本題學到的觀念

### 6.1 HashSet 的用途

當問題只需要判斷某個元素是否存在，不需要額外儲存與該元素對應的 Value 時，可以考慮使用 HashSet。

### 6.2 Contains() 與 Add()

- `Contains()`：判斷元素是否存在。
- `Add()`：將元素加入集合。

這兩個方法搭配使用，可以有效率地判斷元素是否曾經出現。

### 6.3 提早結束程式

當已經找到重複元素時，使用 `return true` 直接結束方法。

不需要為了檢查完整個陣列而繼續執行。

### 6.4 時間與空間的取捨

暴力解法不需要額外的集合，但比較次數較多。

HashSet 解法需要額外空間，但可以降低平均時間複雜度。

選擇解法時，應該同時考慮執行效率與空間使用量。

---
