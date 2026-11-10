# LeetCode 11 - Container With Most Water

## 題目說明

給定一個整數陣列 `height`，陣列中的每個元素代表一條垂直線的高度。

選擇兩條垂直線，與 X 軸形成一個容器，計算容器最多能裝多少水。

容器的面積取決於兩條線之間的距離，以及較短線的高度。

**計算公式：**

```text
Area = min(height[left], height[right]) * (right - left)
```

## 解法：Two Pointers（雙指標）

### 解題思路

使用左右雙指標，分別從陣列的兩端開始搜尋。

1. 初始化 `left = 0`，`right = height.Length - 1`。
2. 計算目前左右指標所形成的容器面積。
3. 使用 `Math.Max()` 更新最大面積。
4. 比較左右兩側的高度，移動較短的一側。
5. 當 `left >= right` 時，代表搜尋結束，回傳最大面積。

### 為什麼移動較短的一側？

容器的高度由左右兩條線中較短的一側決定。

假設左側高度為 8，右側高度為 3：

- 容器高度受到右側高度 3 的限制。
- 如果移動左側指標，寬度會縮小，但容器高度仍然不可能超過 3。
- 如果移動右側指標，則有機會找到更高的線，進而增加容器面積。

因此，每次都應該移動較短的一側，避免進行沒有機會改善答案的搜尋。

## C# 程式碼

```csharp
/// <summary>
/// 計算兩條垂直線與 X 軸所能形成的最大容器面積。
/// </summary>
/// <param name="height">每條垂直線的高度陣列。</param>
/// <returns>可以形成的最大容器面積。</returns>
public int MaxArea(int[] height)
{
    int left = 0;
    int right = height.Length - 1;
    int maxArea = 0;

    while (left < right)
    {
        int area = Math.Min(height[left], height[right]) * (right - left);

        maxArea = Math.Max(maxArea, area);

        if (height[left] < height[right])
        {
            left++;
        }
        else
        {
            right--;
        }
    }

    return maxArea;
}
```

## 複雜度分析

| 項目 | 複雜度 | 說明 |
|---|---|---|
| 時間複雜度 | O(n) | 每次迴圈都會移動一個指標，最多執行 n-1 次 |
| 空間複雜度 | O(1) | 只使用固定數量的變數 |

## 學習心得

這題學習到雙指標（Two Pointers）的應用方式。

與暴力解法需要計算所有可能的左右組合不同，雙指標透過觀察容器面積的限制條件，判斷哪一側可以安全地移動，排除不可能產生更大面積的組合。

**核心觀念：**

- 容器面積由較短的線決定。
- 每次移動較短的一側。
- 利用雙指標將時間複雜度從 O(n²) 降低至 O(n)。
