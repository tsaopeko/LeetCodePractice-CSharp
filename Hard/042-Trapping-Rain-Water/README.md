# LeetCode 42 - Trapping Rain Water

## 題目說明

給定一個整數陣列 `height`，代表每根柱子的高度，每根柱子的寬度皆為 1。

計算這些柱子在下雨後，最多能夠累積多少單位的雨水。

### 範例

**Example 1**

```text
Input: height = [0,1,0,2,1,0,1,3,2,1,2,1]
Output: 6
```

**Example 2**

```text
Input: height = [4,2,0,3,2,5]
Output: 9
```

---

## 解法一：Prefix Maximum + Suffix Maximum

### 解題思路

每個位置能夠累積的雨水量，取決於該位置左側與右側的最高柱子。

計算方式：

```text
water = min(leftMax[i], rightMax[i]) - height[i]
```

其中：

- `leftMax[i]`：目前位置左側（包含自己）的最高柱子。
- `rightMax[i]`：目前位置右側（包含自己）的最高柱子。
- `height[i]`：目前柱子的高度。

### 實作步驟

**1. 建立左右最高高度陣列**

建立兩個陣列：

- `leftMax`：從左往右記錄最高高度。
- `rightMax`：從右往左記錄最高高度。

**2. 計算左側最高高度**

第一個位置沒有前一個位置，因此直接使用自己的高度。

```csharp
leftMax[0] = height[0];

for (int i = 1; i < height.Length; i++)
{
    leftMax[i] = Math.Max(leftMax[i - 1], height[i]);
}
```

**3. 計算右側最高高度**

最後一個位置沒有後一個位置，因此直接使用自己的高度。

```csharp
rightMax[height.Length - 1] = height[height.Length - 1];

for (int i = height.Length - 2; i >= 0; i--)
{
    rightMax[i] = Math.Max(rightMax[i + 1], height[i]);
}
```

**4. 計算總積水量**

遍歷每個位置，計算可累積的雨水量並加總。

```csharp
int totalWater = 0;

for (int i = 0; i < height.Length; i++)
{
    totalWater += Math.Min(leftMax[i], rightMax[i]) - height[i];
}
```

### 完整程式碼

```csharp
/// <summary>
/// 計算柱狀圖中可以累積的最大雨水量。
/// </summary>
/// <param name="height">代表每根柱子高度的整數陣列。</param>
/// <returns>可以累積的總雨水量。</returns>
public int Trap(int[] height)
{
    if (height == null || height.Length < 3)
    {
        return 0;
    }

    int[] leftMax = new int[height.Length];
    int[] rightMax = new int[height.Length];
    int totalWater = 0;

    leftMax[0] = height[0];
    rightMax[height.Length - 1] = height[height.Length - 1];

    for (int i = 1; i < height.Length; i++)
    {
        leftMax[i] = Math.Max(leftMax[i - 1], height[i]);
    }

    for (int i = height.Length - 2; i >= 0; i--)
    {
        rightMax[i] = Math.Max(rightMax[i + 1], height[i]);
    }

    for (int i = 0; i < height.Length; i++)
    {
        totalWater += Math.Min(leftMax[i], rightMax[i]) - height[i];
    }

    return totalWater;
}
```

### 複雜度分析

| 項目 | 複雜度 | 原因 |
|---|---|---|
| 時間複雜度 | O(n) | 三次線性遍歷 |
| 空間複雜度 | O(n) | 建立兩個長度為 n 的陣列 |

---

## 解法二：Two Pointers（雙指標）

### 解題思路

解法一需要額外建立兩個陣列儲存左右最高高度。

觀察後可以發現，不需要預先儲存所有位置的最高高度，也能利用左右指標逐步計算積水量。

使用兩個指標：

- `left`：從陣列左側開始。
- `right`：從陣列右側開始。

另外使用兩個變數：

- `leftMax`：目前左側走過的最高柱子。
- `rightMax`：目前右側走過的最高柱子。

### 核心判斷

比較左右兩端目前的高度：

```csharp
if (height[left] < height[right])
```

如果左側較矮：

1. 更新左側最高高度。
2. 計算左側目前位置的積水量。
3. 將 `left` 向右移動。

如果右側較矮或兩側相等：

1. 更新右側最高高度。
2. 計算右側目前位置的積水量。
3. 將 `right` 向左移動。

### 為什麼移動較矮的一側？

水位受到左右兩側較矮的最高柱子限制。

當左側目前高度小於右側目前高度時，右側至少存在一根不低於左側目前高度的柱子，因此左側目前位置的積水量可以由左側已知的最高高度決定。

反過來，右側較矮時也是相同道理。

因此，每次都可以先處理較矮的一側，不需要重新搜尋另一側的最高高度。

### 完整程式碼

```csharp
/// <summary>
/// 使用雙指標計算柱狀圖中可以累積的最大雨水量。
/// </summary>
/// <param name="height">代表每根柱子高度的整數陣列。</param>
/// <returns>可以累積的總雨水量。</returns>
public int Trap(int[] height)
{
    if (height == null || height.Length < 3)
    {
        return 0;
    }

    int left = 0;
    int right = height.Length - 1;

    int leftMax = 0;
    int rightMax = 0;
    int totalWater = 0;

    while (left < right)
    {
        if (height[left] < height[right])
        {
            leftMax = Math.Max(leftMax, height[left]);
            totalWater += leftMax - height[left];
            left++;
        }
        else
        {
            rightMax = Math.Max(rightMax, height[right]);
            totalWater += rightMax - height[right];
            right--;
        }
    }

    return totalWater;
}
```

### 複雜度分析

| 項目 | 複雜度 | 原因 |
|---|---|---|
| 時間複雜度 | O(n) | 左右指標最多遍歷陣列一次 |
| 空間複雜度 | O(1) | 只使用固定數量的變數 |

---

## 兩種解法比較

| 比較項目 | Prefix / Suffix Maximum | Two Pointers |
|---|---|---|
| 時間複雜度 | O(n) | O(n) |
| 空間複雜度 | O(n) | O(1) |
| 額外陣列 | 需要 | 不需要 |
| 核心技巧 | 預先計算左右最高值 | 雙指標與動態更新最高值 |

## 學習心得

這題主要學習到以下幾個觀念：

1. **左右最高值的概念**：每個位置的積水量取決於左右兩側最高柱子的較小值。
2. **Prefix / Suffix Maximum**：可以透過正向與反向遍歷，預先計算每個位置的最高高度。
3. **雙指標技巧**：透過左右指標，逐步處理積水量已經可以確定的一側。
4. **空間複雜度優化**：從使用兩個額外陣列的 O(n)，優化成只使用固定變數的 O(1)。


## 結論

兩種解法都能達到 O(n) 時間複雜度。

第一種解法比較直觀，適合理解左右最高高度與積水量的關係；第二種解法則進一步減少額外空間，是這題更有效率的實作方式。