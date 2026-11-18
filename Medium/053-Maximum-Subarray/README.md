

# LeetCode 53 - Maximum Subarray

**Difficulty:** Medium  
**Topics:** Array、Dynamic Programming、Kadane's Algorithm

## 題目說明

給定一個整數陣列 `nums`，找出一個具有最大總和的連續子陣列（至少包含一個元素），並回傳其總和。

### 範例

**Example 1**

```text
Input: nums = [-2,1,-3,4,-1,2,1,-5,4]
Output: 6
```

最大總和的連續子陣列為：

```text
[4, -1, 2, 1]
```

總和為：

```text
4 + (-1) + 2 + 1 = 6
```

**Example 2**

```text
Input: nums = [1]
Output: 1
```

**Example 3**

```text
Input: nums = [5,4,-1,7,8]
Output: 23
```

---

## 解法一：暴力法（Brute Force）

### 解題思路

透過兩層迴圈，分別選擇連續子陣列的起點與終點，再計算該子陣列的總和。

最直接的做法是每次選定起點與終點後，再使用第三層迴圈重新計算總和。

### C# 程式碼

```csharp
/// <summary>
/// 使用暴力法找出連續子陣列的最大總和。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>連續子陣列的最大總和。</returns>
public int MaxSubArray(int[] nums)
{
    int maxSum = int.MinValue;

    for (int i = 0; i < nums.Length; i++)
    {
        for (int j = i; j < nums.Length; j++)
        {
            int currentSum = 0;

            for (int k = i; k <= j; k++)
            {
                currentSum += nums[k];
            }

            maxSum = Math.Max(maxSum, currentSum);
        }
    }

    return maxSum;
}
```

### 複雜度分析

- 時間複雜度：`O(n³)`
- 空間複雜度：`O(1)`

---

## 解法二：累加優化（Incremental Sum）

### 解題思路

解法一每次選定子陣列後，都需要重新計算總和。

但其實可以在內層迴圈中，持續累加目前的數字，不需要重新走訪子陣列。

例如：

```text
nums = [1, 2, 3]
```

以索引 0 作為起點：

```text
[1]       => 1
[1, 2]    => 3
[1, 2, 3] => 6
```

每次只需要將新的數字加進目前總和即可。

### C# 程式碼

```csharp
/// <summary>
/// 使用累加方式找出連續子陣列的最大總和。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>連續子陣列的最大總和。</returns>
public int MaxSubArray(int[] nums)
{
    int maxSum = int.MinValue;

    for (int i = 0; i < nums.Length; i++)
    {
        int currentSum = 0;

        for (int j = i; j < nums.Length; j++)
        {
            currentSum += nums[j];

            maxSum = Math.Max(maxSum, currentSum);
        }
    }

    return maxSum;
}
```

### 複雜度分析

- 時間複雜度：`O(n²)`
- 空間複雜度：`O(1)`

---

## 解法三：Kadane's Algorithm（Dynamic Programming）

### 解題思路

觀察前兩種解法後，可以發現不一定需要嘗試每一種子陣列。

如果目前的累積總和小於 0，代表它會降低後續子陣列的總和，因此可以捨棄前面的累積結果，直接從目前元素重新開始。

反之，如果目前累積總和大於或等於 0，就保留前面的結果並繼續累加。

使用兩個變數：

- `currentSum`：以目前元素結尾的最大子陣列總和。
- `maxSum`：目前找到的最大子陣列總和。

### C# 程式碼

```csharp
/// <summary>
/// 使用 Kadane's Algorithm 找出連續子陣列的最大總和。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>連續子陣列的最大總和。</returns>
public int MaxSubArray(int[] nums)
{
    int currentSum = nums[0];
    int maxSum = nums[0];

    for (int i = 1; i < nums.Length; i++)
    {
        if (currentSum < 0)
        {
            currentSum = nums[i];
        }
        else
        {
            currentSum += nums[i];
        }

        maxSum = Math.Max(maxSum, currentSum);
    }

    return maxSum;
}
```

### 複雜度分析

- 時間複雜度：`O(n)`
- 空間複雜度：`O(1)`

---

## 三種解法比較

| 解法 | 時間複雜度 | 空間複雜度 |
|---|---|---|
| 暴力法 | O(n³) | O(1) |
| 累加優化 | O(n²) | O(1) |
| Kadane's Algorithm | O(n) | O(1) |

## 學習重點

1. 理解連續子陣列的起點與終點。
2. 使用累加方式避免重複計算子陣列總和。
3. 理解負數累積總和對後續結果的影響。
4. 學習 Kadane's Algorithm，透過局部最佳解找出全域最大值。
5. 使用 `int.MinValue` 或第一個陣列元素初始化最大總和，避免全部元素都是負數時錯誤回傳 0。

## 心得

這題從暴力法開始，逐步優化到 Kadane's Algorithm。

一開始需要透過多層迴圈列舉所有連續子陣列，後來發現可以利用累加避免重複計算，最後進一步觀察到：

**當目前累積總和為負數時，保留它只會讓後續結果變小，因此應該捨棄並重新開始。**

透過這個觀察，將時間複雜度從 O(n²) 降低到 O(n)，也進一步理解了動態規劃的概念。

