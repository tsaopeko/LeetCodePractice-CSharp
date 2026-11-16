# LeetCode 121 - Best Time to Buy and Sell Stock

## 題目說明

給定一個整數陣列 `prices`，其中 `prices[i]` 代表第 `i` 天的股票價格。

只能選擇某一天買入股票，並在未來某一天賣出股票，求能獲得的最大利潤。

如果無法獲利，則回傳 `0`。

### 範例

**Example 1**

```csharp
Input: prices = [7,1,5,3,6,4]
Output: 5
```

在第 2 天以價格 `1` 買入，在第 5 天以價格 `6` 賣出，最大利潤為 `6 - 1 = 5`。

**Example 2**

```csharp
Input: prices = [7,6,4,3,1]
Output: 0
```

股價持續下跌，無法獲利，因此回傳 `0`。

---

## 解法一：暴力法（Brute Force）

### 解題思路

使用兩層 `for` 迴圈，列舉所有可能的買入與賣出日期。

1. 外層迴圈選擇買入日期 `i`。
2. 內層迴圈從 `i + 1` 開始，選擇賣出日期 `j`。
3. 計算兩天之間的價差。
4. 如果目前利潤大於 `maxProfit`，就更新最大利潤。

### C# 程式碼

```csharp
/// <summary>
/// 使用暴力法計算股票交易的最大利潤。
/// </summary>
/// <param name="prices">每日股票價格陣列。</param>
/// <returns>可以獲得的最大利潤。</returns>
public int MaxProfitBruteForce(int[] prices)
{
    int maxProfit = 0;

    for (int i = 0; i < prices.Length; i++)
    {
        for (int j = i + 1; j < prices.Length; j++)
        {
            int profit = prices[j] - prices[i];

            if (profit > maxProfit)
            {
                maxProfit = profit;
            }
        }
    }

    return maxProfit;
}
```

### 複雜度分析

- 時間複雜度：`O(n²)`
  - 使用兩層迴圈列舉所有可能的買賣組合。
- 空間複雜度：`O(1)`
  - 只使用固定數量的變數，不需要額外陣列或集合。

---

## 解法二：單次遍歷（One Pass）

### 解題思路

觀察暴力法可以發現，當我們選擇某一天作為賣出日期時，只需要知道前面出現過的最低價格，就能計算出當天可能獲得的最大利潤。

因此不需要重新比較所有買入日期，只要從左到右遍歷一次陣列即可。

使用兩個變數：

- `minPrice`：記錄目前看過的最低股票價格。
- `maxProfit`：記錄目前能獲得的最大利潤。

每次遍歷時執行三個步驟：

1. 使用目前最低價格計算今天賣出的利潤。
2. 更新目前最大利潤。
3. 更新目前最低股票價格。

### C# 程式碼

```csharp
/// <summary>
/// 使用單次遍歷計算股票交易的最大利潤。
/// </summary>
/// <param name="prices">每日股票價格陣列。</param>
/// <returns>可以獲得的最大利潤。</returns>
public int MaxProfit(int[] prices)
{
    int minPrice = prices[0];
    int maxProfit = 0;

    for (int i = 1; i < prices.Length; i++)
    {
        int profit = prices[i] - minPrice;

        maxProfit = Math.Max(maxProfit, profit);

        minPrice = Math.Min(minPrice, prices[i]);
    }

    return maxProfit;
}
```

### 複雜度分析

- 時間複雜度：`O(n)`
  - 只需要遍歷陣列一次，每次操作都是常數時間。
- 空間複雜度：`O(1)`
  - 只使用 `minPrice`、`maxProfit` 和 `profit` 等固定數量的變數。

---

## 兩種解法比較

| 比較項目 | 暴力法 | 單次遍歷 |
|---|---|---|
| 核心概念 | 列舉所有買賣組合 | 記錄目前最低價格 |
| 迴圈數量 | 兩層 | 一層 |
| 時間複雜度 | O(n²) | O(n) |
| 空間複雜度 | O(1) | O(1) |

---

## 學習心得

這題最重要的觀念是：不一定需要保存所有可能的買賣組合。

透過觀察可以發現，對於每一天的賣出價格，只需要知道之前出現過的最低買入價格，就能計算出當天最大的可能利潤。

因此可以使用 `minPrice` 保存目前最低價格，將原本需要兩層迴圈的暴力法，優化成只需要一次遍歷的解法。

這題也讓我練習到：

1. 使用兩層迴圈列舉所有可能組合。
2. 分析暴力法的時間複雜度。
3. 透過保存有用的資訊減少重複計算。
4. 使用單次遍歷將時間複雜度從 `O(n²)` 優化至 `O(n)`。
