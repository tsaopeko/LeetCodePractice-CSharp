# LeetCode 238 - Product of Array Except Self

## 題目描述

給定一個整數陣列 `nums`，回傳一個陣列 `answer`，其中：

`answer[i]` 等於原陣列中除了 `nums[i]` 以外，所有元素的乘積。

**限制條件：**
- 不可以使用除法。
- 時間複雜度必須為 O(n)。
- 不需要額外建立另一個乘積陣列來儲存左右乘積。

## 範例

**Input**
```csharp
nums = [1, 2, 3, 4]
```

**Output**
```csharp
[24, 12, 8, 6]
```

## 解題思路

### 方法：Prefix Product + Suffix Product

將每個位置的答案拆成兩個部分：

- **Prefix Product（前綴乘積）：** 目前索引左側所有元素的乘積。
- **Suffix Product（後綴乘積）：** 目前索引右側所有元素的乘積。

最後將左右兩側的乘積相乘，就能得到排除目前索引元素後的結果。

### 第一輪：計算左側乘積

從左往右遍歷陣列，利用前一個位置的結果計算目前位置的左側乘積。

```csharp
result[0] = 1;

for (int i = 1; i < nums.Length; i++)
{
    result[i] = result[i - 1] * nums[i - 1];
}
```

以 `[1, 2, 3, 4]` 為例：

```text
result = [1, 1, 2, 6]
```

### 第二輪：計算右側乘積

從右往左遍歷，使用 `rightProduct` 記錄目前位置右側所有元素的乘積。

```csharp
int rightProduct = 1;

for (int i = nums.Length - 1; i >= 0; i--)
{
    result[i] *= rightProduct;
    rightProduct *= nums[i];
}
```

注意：
1. 先將目前位置的左側乘積乘上右側乘積。
2. 再將目前元素乘入 `rightProduct`，供下一個位置使用。

### 計算過程

| 索引 | 左側乘積 | 右側乘積 | 最終答案 |
|---|---:|---:|---:|
| 0 | 1 | 24 | 24 |
| 1 | 1 | 12 | 12 |
| 2 | 2 | 4 | 8 |
| 3 | 6 | 1 | 6 |

最終結果：

```text
[24, 12, 8, 6]
```

## 完整 C# 程式碼

```csharp
public class Solution
{
    /// <summary>
    /// 計算陣列中每個元素以外的所有元素乘積。
    /// </summary>
    /// <param name="nums">輸入的整數陣列。</param>
    /// <returns>每個位置排除自身後的乘積陣列。</returns>
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] result = new int[nums.Length];
        result[0] = 1;

        int rightProduct = 1;

        // 第一輪：計算左側乘積
        for (int i = 1; i < nums.Length; i++)
        {
            result[i] = result[i - 1] * nums[i - 1];
        }

        // 第二輪：乘上右側乘積
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            result[i] = result[i] * rightProduct;
            rightProduct *= nums[i];
        }

        return result;
    }
}
```

## 複雜度分析

| 項目 | 複雜度 | 說明 |
|---|---|---|
| 時間複雜度 | O(n) | 遍歷陣列兩次，每次都是 O(n)。 |
| 空間複雜度 | O(1) | 不計算輸出陣列，只使用固定數量的額外變數。 |

## 學習重點

1. 理解 Prefix Product 與 Suffix Product 的概念。
2. 利用前一個位置的結果，避免重複計算。
3. 透過左右兩側乘積排除目前索引的元素。
4. 不使用除法也能完成乘積計算。
5. 使用兩次線性遍歷，達到 O(n) 時間複雜度。

## 心得

這題一開始想到的是，對每個索引重新遍歷整個陣列，跳過目前索引的元素後再計算乘積，但這樣會產生 O(n²) 的時間複雜度。

後來透過前綴乘積與後綴乘積的概念，將計算拆成左右兩個方向：

- 從左往右，先記錄每個位置的左側乘積。
- 從右往左，再乘上每個位置的右側乘積。

透過這種方式，不需要重複遍歷，也不需要使用除法，就能完成題目要求。

**這題讓我學習到，適當地保存前一次計算的結果，可以有效降低重複運算，將 O(n²) 的解法優化成 O(n)。**