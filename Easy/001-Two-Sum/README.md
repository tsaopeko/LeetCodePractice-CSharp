# 001. Two Sum

LeetCode 題目：Two Sum

難度：Easy

---

## 題目說明

給定一個整數陣列 `nums` 和一個整數 `target`，找出陣列中兩個數字，使兩個數字相加等於 `target`。

回傳這兩個數字的索引。

### 範例

```text
Input:
nums = [2, 7, 11, 15]
target = 9

Output:
[0, 1]
```

因為：

```text
nums[0] + nums[1]
= 2 + 7
= 9
```

---

## 解題思路

### 方法一：暴力解法

一開始先使用兩層 `for` 迴圈。

第一層選擇第一個數字，第二層尋找第二個數字。

如果：

```csharp
nums[i] + nums[j] == target
```

就回傳 `i` 和 `j`。

這種方法可以解決問題，但是需要使用兩層迴圈。

### 時間複雜度

```text
O(n²)
```

### 空間複雜度

```text
O(1)
```

---

## 方法二：Dictionary 最佳化

使用 `Dictionary<int, int>` 儲存：

```text
Key   = 數字
Value = 數字的索引
```

例如：

```text
2 → 0
7 → 1
11 → 2
```

每次取得 `nums[i]` 後，先計算需要的另一個數字：

```csharp
int complement = target - nums[i];
```

例如：

```text
target = 9
nums[i] = 2

complement = 9 - 2
           = 7
```

接著使用：

```csharp
map.ContainsKey(complement)
```

確認之前是否看過這個數字。

如果找到，就可以直接取得它的索引。

---

## 為什麼使用 Dictionary？

原本使用兩層 `for` 迴圈，需要一直搜尋其他元素：

```text
O(n²)
```

使用 Dictionary 後，可以快速確認某個數字是否已經出現過。

因此可以將時間複雜度降低到：

```text
O(n)
```

這是一種：

> 用空間換取時間

的解題方式。

---

## C# 解法

```csharp
public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (map.ContainsKey(complement))
            {
                return new int[] { map[complement], i };
            }

            map[nums[i]] = i;
        }

        return new int[] {};
    }
}
```

---

## 執行流程

以：

```text
nums = [3, 2, 4]
target = 6
```

為例。

### i = 0

```text
nums[i] = 3
complement = 6 - 3 = 3

map = {}
```

找不到 `3`，因此加入：

```text
3 → 0
```

---

### i = 1

```text
nums[i] = 2
complement = 6 - 2 = 4

map = {
    3 → 0
}
```

找不到 `4`，加入：

```text
2 → 1
```

現在：

```text
map = {
    3 → 0,
    2 → 1
}
```

---

### i = 2

```text
nums[i] = 4
complement = 6 - 4 = 2
```

Dictionary 中存在：

```text
2 → 1
```

因此：

```text
return [1, 2]
```

驗證：

```text
nums[1] + nums[2]
= 2 + 4
= 6
```

---

## 複雜度分析

| 項目               | 複雜度  |
| ---------------- | ---- |
| Time Complexity  | O(n) |
| Space Complexity | O(n) |

---

## 本題學習重點

* Array
* `for` 迴圈
* Dictionary
* `Dictionary<TKey, TValue>`
* `ContainsKey()`
* Key / Value
* HashMap 概念
* Time Complexity
* Space Complexity
* 用空間換時間

---