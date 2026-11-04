# LeetCode 347 - Top K Frequent Elements

**Difficulty：Medium**

## 1. 題目說明

給定一個整數陣列 `nums` 和一個整數 `k`，請找出陣列中出現頻率最高的 `k` 個元素。

題目要求演算法的時間複雜度必須優於 O(n log n)。

### 範例

**Input**
```csharp
nums = [1, 1, 1, 2, 2, 3]
k = 2
```

**Output**
```csharp
[1, 2]
```

**原因：**

| 數字 | 出現次數 |
|---|---:|
| 1 | 3 |
| 2 | 2 |
| 3 | 1 |

出現頻率最高的兩個數字為 `1` 和 `2`。

---

## 2. 解題思考

這題的核心在於：

**如何找出陣列中出現頻率最高的 K 個數字？**

首先需要知道每個數字出現的次數，因此使用 Dictionary 統計頻率。

```csharp
Dictionary<int, int> frequency =
    new Dictionary<int, int>();
```

- Key：儲存陣列中的數字。
- Value：儲存該數字出現的次數。

接著思考如何從統計結果中找出頻率最高的 K 個元素。

本題使用兩種解法：

1. Dictionary + Sorting
2. Dictionary + Bucket Sort

---

## 3. 解法一：Dictionary + Sorting

### 解題思路

第一步先使用 Dictionary 統計每個數字出現的次數。

```csharp
if (frequency.ContainsKey(nums[i]))
{
    frequency[nums[i]]++;
}
else
{
    frequency.Add(nums[i], 1);
}
```

統計完成後：

```text
1 → 3 次
2 → 2 次
3 → 1 次
```

接著將 Dictionary 轉換成 List，並依照 Value 由大到小排序。

最後使用 LINQ 的 `Take(k)` 取得前 K 筆，再使用 `Select()` 取出 Key。

### 程式碼

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int, int> frequency =
            new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (frequency.ContainsKey(nums[i]))
            {
                frequency[nums[i]]++;
            }
            else
            {
                frequency.Add(nums[i], 1);
            }
        }

        List<KeyValuePair<int, int>> list =
            frequency.ToList();

        list = list.OrderByDescending(x => x.Value).ToList();

        int[] result = list
            .Take(k)
            .Select(x => x.Key)
            .ToArray();

        return result;
    }
}
```

### 程式碼解析

**1. Dictionary 統計頻率**

```csharp
if (frequency.ContainsKey(nums[i]))
{
    frequency[nums[i]]++;
}
else
{
    frequency.Add(nums[i], 1);
}
```

如果數字已經存在，就將出現次數加一；如果不存在，就新增並將次數設定為 1。

**2. Dictionary 轉換成 List**

```csharp
List<KeyValuePair<int, int>> list =
    frequency.ToList();
```

使用 `KeyValuePair<int, int>` 同時保留數字及其出現次數。

**3. 依照出現次數排序**

```csharp
list = list.OrderByDescending(x => x.Value).ToList();
```

使用 `OrderByDescending()` 依照 Value 由大到小排序。

**4. 取得前 K 個元素**

```csharp
int[] result = list
    .Take(k)
    .Select(x => x.Key)
    .ToArray();
```

- `Take(k)`：取得前 K 筆資料。
- `Select(x => x.Key)`：取出數字。
- `ToArray()`：將結果轉換成陣列。

### 時間複雜度

假設：

- `n`：原始陣列長度。
- `m`：不同數字的數量，且 m ≤ n。

| 操作 | 時間複雜度 |
|---|---|
| Dictionary 統計 | O(n) |
| Dictionary 轉 List | O(m) |
| 排序 | O(m log m) |
| Take 與 Select | O(k) |

整體：

**O(n + m log m)**

最壞情況下，每個數字都不同，m = n。

因此：

**O(n log n)**

### 空間複雜度

Dictionary 和 List 需要儲存不同數字及其頻率，整體為：

**O(n)**

---

## 4. 解法二：Dictionary + Bucket Sort

### 解題思路

排序法雖然能找出頻率最高的 K 個數字，但需要將所有不同數字排序。

進一步思考：

題目只要求找出前 K 個元素，是否一定需要排序？

觀察到一個特性：

**單一數字最多只能出現 nums.Length 次。**

因此，可以使用出現次數作為陣列索引，將數字放入對應的桶子。

例如：

```text
1 → 3 次
2 → 2 次
3 → 1 次
```

建立桶子後：

```text
buckets[1] = [3]
buckets[2] = [2]
buckets[3] = [1]
```

接著從最大的索引開始往下尋找，就能優先取得出現次數最高的數字。

### 程式碼

```csharp
using System;
using System.Collections.Generic;

public class Solution
{
    public int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int, int> frequency =
            new Dictionary<int, int>();

        // 統計每個數字出現的次數
        for (int i = 0; i < nums.Length; i++)
        {
            if (frequency.ContainsKey(nums[i]))
            {
                frequency[nums[i]]++;
            }
            else
            {
                frequency.Add(nums[i], 1);
            }
        }

        // 建立 Bucket
        List<int>[] buckets =
            new List<int>[nums.Length + 1];

        // 將數字放入對應頻率的桶子
        foreach (var item in frequency)
        {
            if (buckets[item.Value] == null)
            {
                buckets[item.Value] = new List<int>();
            }

            buckets[item.Value].Add(item.Key);
        }

        // 從最高頻率開始尋找
        List<int> result = new List<int>();

        for (int i = buckets.Length - 1; i >= 0; i--)
        {
            if (buckets[i] != null)
            {
                foreach (int num in buckets[i])
                {
                    result.Add(num);

                    if (result.Count == k)
                    {
                        break;
                    }
                }
            }

            if (result.Count == k)
            {
                break;
            }
        }

        return result.ToArray();
    }
}
```

### 程式碼解析

**1. 建立 Bucket 陣列**

```csharp
List<int>[] buckets =
    new List<int>[nums.Length + 1];
```

為什麼使用 `nums.Length + 1`？

因為陣列索引從 0 開始，而單一數字最多可能出現 `nums.Length` 次。

假設陣列長度為 6，就需要索引 0 到 6，因此需要 7 個位置。

**2. 將數字放入對應的桶子**

```csharp
foreach (var item in frequency)
{
    if (buckets[item.Value] == null)
    {
        buckets[item.Value] = new List<int>();
    }

    buckets[item.Value].Add(item.Key);
}
```

- `item.Key`：原始數字。
- `item.Value`：出現次數。
- `buckets[item.Value]`：對應頻率的桶子。

因為相同頻率可能有多個數字，所以使用 `List<int>` 儲存。

另外，桶子一開始都是 null，因此需要先建立 List，才能使用 `Add()`。

**3. 從最大索引往下搜尋**

```csharp
for (int i = buckets.Length - 1; i >= 0; i--)
```

因為索引代表出現次數，所以從最大索引開始，就能優先找到高頻率的數字。

**4. 收集前 K 個元素**

```csharp
foreach (int num in buckets[i])
{
    result.Add(num);

    if (result.Count == k)
    {
        break;
    }
}
```

當結果數量達到 K，就不需要繼續讀取目前桶子的其他元素。

外層也需要再次判斷：

```csharp
if (result.Count == k)
{
    break;
}
```

這樣才能停止繼續搜尋其他桶子。

**5. 回傳結果**

```csharp
return result.ToArray();
```

使用 `ToArray()` 將 List 轉換成題目要求的 `int[]`。

### 時間複雜度

假設：

- `n`：原始陣列長度。
- `m`：不同數字的數量，且 m ≤ n。

| 操作 | 時間複雜度 |
|---|---|
| Dictionary 統計 | O(n) |
| 建立 Bucket 陣列 | O(n) |
| 將 Dictionary 放入 Bucket | O(m) |
| 從 Bucket 搜尋 | O(n) |
| 回傳結果 | O(k) |

整體：

**O(n + m + n + k) = O(n)**

因為 m 和 k 都不會大於 n，所以最終時間複雜度為：

**O(n)**

### 空間複雜度

| 資料結構 | 空間複雜度 |
|---|---|
| Dictionary | O(m) |
| Bucket 陣列 | O(n) |
| Result | O(k) |

整體：

**O(n)**

---

## 5. 兩種解法比較

| 比較項目 | Sorting | Bucket Sort |
|---|---|---|
| 核心概念 | 依照頻率排序 | 頻率對應陣列索引 |
| Dictionary | 使用 | 使用 |
| 排序 | 需要 | 不需要 |
| 時間複雜度 | O(n log n) | O(n) |
| 空間複雜度 | O(n) | O(n) |
| 優點 | 容易理解、實作簡單 | 不需要完整排序，效率較高 |

---

## 6. 學習重點

透過這題練習，理解了以下 C# 與演算法概念：

1. `Dictionary<int, int>`：統計每個數字出現的次數。
2. `ContainsKey()`：判斷 Dictionary 是否存在指定的 Key。
3. `KeyValuePair<int, int>`：同時保存數字與出現次數。
4. `ToList()`：將 Dictionary 轉換成 List。
5. `OrderByDescending()`：依照指定欄位降冪排序。
6. `Take(k)`：取得前 K 筆資料。
7. `Select()`：選取需要的欄位。
8. `ToArray()`：將 List 轉換成陣列。
9. Bucket Sort：利用出現頻率作為陣列索引，避免完整排序。
10. `List<int>[]`：使用陣列管理多個 List。