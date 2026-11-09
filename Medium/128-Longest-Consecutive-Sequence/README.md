# LeetCode 128 - Longest Consecutive Sequence（最長連續序列）

## 題目說明

給定一個未排序的整數陣列 `nums`，找出其中最長的連續整數序列長度。

連續序列不需要在原始陣列中相鄰。

**範例：**

```csharp
nums = [100, 4, 200, 1, 3, 2];
```

輸出：

```text
4
```

解釋：

最長的連續整數序列為：

```text
1 → 2 → 3 → 4
```

因此回傳長度 `4`。

題目要求時間複雜度為 O(n)。

---

## 解法一：排序法

### 解題思路

先使用 `Array.Sort()` 將陣列由小到大排序，再透過比較相鄰數字的差值，判斷是否連續。

1. 將陣列排序。
2. 使用兩個變數紀錄目前連續序列長度與最大長度。
3. 比較相鄰兩個數字：
   - 差值為 1：連續序列長度 +1。
   - 差值為 0：遇到重複數字，不改變目前長度。
   - 差值大於 1：連續序列中斷，更新最大長度並重新計算。
4. 迴圈結束後，再比較一次最大長度，避免漏掉最後一段序列。

### C# 程式碼

```csharp
/// <summary>
/// 使用排序法找出最長的連續整數序列長度。
/// </summary>
/// <param name="nums">未排序的整數陣列。</param>
/// <returns>最長連續整數序列的長度。</returns>
public int LongestConsecutive(int[] nums)
{
    if (nums == null || nums.Length == 0)
    {
        return 0;
    }

    int currentLength = 1;
    int maxLength = 1;

    Array.Sort(nums);

    for (int i = 0; i < nums.Length - 1; i++)
    {
        if ((long)nums[i + 1] - nums[i] == 1)
        {
            currentLength++;
        }
        else if ((long)nums[i + 1] - nums[i] > 1)
        {
            maxLength = Math.Max(maxLength, currentLength);
            currentLength = 1;
        }
    }

    maxLength = Math.Max(maxLength, currentLength);

    return maxLength;
}
```

### 複雜度分析

- 排序：O(n log n)。
- 遍歷陣列：O(n)。
- 更新最大長度：O(1)。

**時間複雜度：O(n log n)**

**額外空間複雜度：** 依排序實作而定；`Array.Sort()` 使用原地排序，但排序本身仍可能需要 O(log n) 的堆疊空間。

此方法容易理解，但不符合題目要求的 O(n) 時間複雜度。

---

## 解法二：HashSet（最佳解法）

### 解題思路

使用 `HashSet<int>` 儲存所有數字，利用 `Contains()` 平均 O(1) 的查找效率，避免排序。

核心概念是：

**只從每一段連續序列的起點開始計算，避免重複搜尋相同序列。**

### 1. 建立 HashSet

```csharp
HashSet<int> set = new HashSet<int>(nums);
```

HashSet 會自動移除重複數字。

例如：

```text
nums = [1, 2, 2, 3, 4, 4, 5]

set = {1, 2, 3, 4, 5}
```

### 2. 判斷連續序列起點

```csharp
if (!set.Contains(num - 1))
```

如果目前數字的前一個數字不存在於 HashSet，代表目前數字就是一段連續序列的起點。

例如：

```text
1 → 2 → 3 → 4
```

只有 `1` 的前一個數字 `0` 不存在，因此只需要從 `1` 開始計算。

### 3. 使用 while 延伸連續序列

```csharp
while (set.Contains(current + 1))
{
    current++;
    currentLength++;
}
```

從起點開始，不斷確認下一個數字是否存在。

只要存在，就將目前數字與連續序列長度加一，直到下一個數字不存在為止。

### 4. 更新最大長度

```csharp
maxLength = Math.Max(maxLength, currentLength);
```

每完成一段連續序列，就更新目前找到的最大長度。

### C# 程式碼

```csharp
/// <summary>
/// 使用 HashSet 找出最長的連續整數序列長度。
/// </summary>
/// <param name="nums">未排序的整數陣列。</param>
/// <returns>最長連續整數序列的長度。</returns>
public int LongestConsecutive(int[] nums)
{
    if (nums == null || nums.Length == 0)
    {
        return 0;
    }

    HashSet<int> set = new HashSet<int>(nums);
    int maxLength = 0;

    foreach (int num in set)
    {
        if (num != int.MinValue && set.Contains(num - 1))
        {
            continue;
        }

        int current = num;
        int currentLength = 1;

        while (current != int.MaxValue && set.Contains(current + 1))
        {
            current++;
            currentLength++;
        }

        maxLength = Math.Max(maxLength, currentLength);
    }

    return maxLength;
}
```

### 複雜度分析

- 建立 HashSet：O(n)。
- 遍歷 HashSet：O(n)。
- 搜尋連續數字：所有序列的總搜尋次數為 O(n)。
- 更新最大長度：O(1)。

**時間複雜度：O(n) 平均**

**空間複雜度：O(n)**

符合題目要求的 O(n) 時間複雜度。

---

## 兩種解法比較

| 比較項目 | 排序法 | HashSet |
|---|---|---|
| 核心技術 | Array.Sort | HashSet |
| 時間複雜度 | O(n log n) | O(n) 平均 |
| 額外空間 | 依排序實作而定 | O(n) |
| 重複數字處理 | 跳過差值為 0 | HashSet 自動去重 |
| 是否符合題目要求 | 否 | 是 |

## 學習重點

1. 使用 HashSet 快速查找元素是否存在。
2. 利用 HashSet 自動移除重複元素的特性。
3. 透過 `num - 1` 是否存在，判斷連續序列的起點。
4. 使用 `while` 從起點向後延伸連續序列。
5. 避免重複計算，將排序法優化為平均 O(n)。
6. 注意 `int.MinValue` 與 `int.MaxValue` 的整數溢位問題。

## 使用技術

- C#
- HashSet
- Array.Sort
- foreach
- while
- Math.Max
- 時間與空間複雜度分析