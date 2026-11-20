

# LeetCode 152 - Maximum Product Subarray

**Difficulty:** Medium  
**Topics:** Array、Dynamic Programming

## 題目說明

給定一個整數陣列 `nums`，找出具有最大乘積的連續子陣列，並回傳該子陣列的乘積。

子陣列必須至少包含一個元素。

### 範例

**Example 1**

```text
Input: nums = [2,3,-2,4]
Output: 6
```

最大乘積的連續子陣列為 `[2,3]`，乘積為 `6`。

**Example 2**

```text
Input: nums = [-2,0,-1]
Output: 0
```

最大乘積的連續子陣列為 `[0]`，乘積為 `0`。

**Example 3**

```text
Input: nums = [-2,3,-4]
Output: 24
```

最大乘積的連續子陣列為 `[-2,3,-4]`，乘積為 `24`。

---

## 解法一：暴力法（Brute Force）

### 解題思路

使用兩層迴圈，分別選擇連續子陣列的起點與終點。

每次固定起點後，使用第三層迴圈重新計算子陣列的乘積，並更新最大乘積。

### C# 程式碼

```csharp
/// <summary>
/// 使用暴力法找出連續子陣列的最大乘積。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>連續子陣列的最大乘積。</returns>
public int MaxProduct(int[] nums)
{
    int result = int.MinValue;

    for (int i = 0; i < nums.Length; i++)
    {
        for (int j = i; j < nums.Length; j++)
        {
            int currentProduct = 1;

            for (int k = i; k <= j; k++)
            {
                currentProduct *= nums[k];
            }

            result = Math.Max(result, currentProduct);
        }
    }

    return result;
}
```

### 複雜度分析

- 時間複雜度：`O(n³)`
- 空間複雜度：`O(1)`

---

## 解法二：累乘優化（Incremental Product）

### 解題思路

暴力法每次選定子陣列後，都需要重新計算乘積。

可以改成使用一個變數持續累乘，避免重複計算。

每次固定新的起點時，將 `currentProduct` 初始化為 `1`，再逐步乘上後續元素。

### C# 程式碼

```csharp
/// <summary>
/// 使用累乘方式找出連續子陣列的最大乘積。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>連續子陣列的最大乘積。</returns>
public int MaxProduct(int[] nums)
{
    int result = int.MinValue;

    for (int i = 0; i < nums.Length; i++)
    {
        int currentProduct = 1;

        for (int j = i; j < nums.Length; j++)
        {
            currentProduct *= nums[j];

            result = Math.Max(result, currentProduct);
        }
    }

    return result;
}
```

### 複雜度分析

- 時間複雜度：`O(n²)`
- 空間複雜度：`O(1)`

---

## 解法三：動態規劃（Dynamic Programming）

### 解題思路

這題與 Maximum Subarray 不同的地方在於：

**負數乘以負數會變成正數，因此目前最小的乘積，未來可能變成最大的乘積。**

所以不能只維護目前的最大乘積，還需要同時維護最小乘積。

使用三個變數：

| 變數 | 用途 |
|---|---|
| `maxProduct` | 以目前位置結尾的最大乘積 |
| `minProduct` | 以目前位置結尾的最小乘積 |
| `result` | 整個陣列目前找到的最大乘積 |

### 狀態轉移

假設目前元素為 `num`，需要比較三種可能：

1. 從目前元素重新開始：`num`
2. 延續前一個最大乘積：`maxProduct * num`
3. 延續前一個最小乘積：`minProduct * num`

新的最大乘積：

```text
newMax = max(num, maxProduct * num, minProduct * num)
```

新的最小乘積：

```text
newMin = min(num, maxProduct * num, minProduct * num)
```

必須先使用 `newMax` 和 `newMin` 儲存計算結果，再更新原本的變數，確保兩者都使用前一個位置的狀態。

### C# 程式碼

```csharp
/// <summary>
/// 使用動態規劃找出連續子陣列的最大乘積。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>連續子陣列的最大乘積。</returns>
public int MaxProduct(int[] nums)
{
    int maxProduct = nums[0];
    int minProduct = nums[0];
    int result = nums[0];

    for (int i = 1; i < nums.Length; i++)
    {
        int newMax = Math.Max(
            nums[i],
            Math.Max(maxProduct * nums[i], minProduct * nums[i]));

        int newMin = Math.Min(
            nums[i],
            Math.Min(maxProduct * nums[i], minProduct * nums[i]));

        maxProduct = newMax;
        minProduct = newMin;

        result = Math.Max(result, maxProduct);
    }

    return result;
}
```

### 複雜度分析

- 時間複雜度：`O(n)`，只需要走訪陣列一次。
- 空間複雜度：`O(1)`，只使用固定數量的變數。

---

## 解法比較

| 解法 | 時間複雜度 | 空間複雜度 |
|---|---|---|
| 暴力法 | O(n³) | O(1) |
| 累乘優化 | O(n²) | O(1) |
| 動態規劃 | O(n) | O(1) |

## 學習重點

1. 使用兩層迴圈列舉連續子陣列。
2. 使用累乘避免重複計算乘積。
3. 理解負數乘法可能讓最小值轉變成最大值。
4. 同時維護最大乘積與最小乘積。
5. 使用新的變數保存狀態轉移結果，避免覆蓋舊資料。
6. 區分目前位置結尾的最大乘積與整個陣列的最大乘積。

## 心得

這題延續了 LeetCode 53 - Maximum Subarray 的動態規劃概念。

在 Maximum Subarray 中，只需要維護目前的最大累積總和；但在 Maximum Product Subarray 中，因為負數相乘會改變正負號，因此必須同時維護最大乘積與最小乘積。

透過這個觀察，將時間複雜度從 O(n²) 優化至 O(n)，並且只需要 O(1) 的額外空間。

