

# LeetCode 198 - House Robber

**Difficulty:** Medium  
**Topics:** Array、Dynamic Programming

## 題目說明

沿著一條街有多間房子，每間房子都有一定金額的財物。

小偷不能在同一個晚上偷竊相鄰的兩間房子，否則會觸發警報。

給定整數陣列 `nums`，其中 `nums[i]` 代表第 `i` 間房子的金額，請計算在不偷竊相鄰房子的情況下，可以取得的最大金額。

### 範例

**Example 1**

```text
Input: nums = [1,2,3,1]
Output: 4
```

選擇第 1 間與第 3 間房子：

```text
1 + 3 = 4
```

**Example 2**

```text
Input: nums = [2,7,9,3,1]
Output: 12
```

選擇第 1、3、5 間房子：

```text
2 + 9 + 1 = 12
```

---

## 解題思路

每間房子都有兩種選擇：

1. **偷竊目前房子：** 取得目前房子的金額，但不能偷前一間房子。
2. **不偷目前房子：** 沿用前一個位置已經計算出的最大金額。

因此，每個位置都需要比較這兩種選擇，找出目前能取得的最大金額。

---

## 解法一：動態規劃（DP Array）

### 解題思路

使用 `dp` 陣列保存每個位置的最佳結果。

定義：

`dp[i]` 代表考慮第 0 間到第 i 間房子時，可以取得的最大金額。

每個位置有兩種選擇：

**選擇偷第 i 間房子**

因為不能偷相鄰房子，所以只能搭配前兩間之前的最佳結果：

```text
dp[i - 2] + nums[i]
```

**選擇不偷第 i 間房子**

沿用前一個位置的最佳結果：

```text
dp[i - 1]
```

### 狀態轉移方程式

```text
dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i])
```

### 初始化

```csharp
dp[0] = nums[0];
dp[1] = Math.Max(nums[0], nums[1]);
```

- `dp[0]`：只有第一間房子可以選擇。
- `dp[1]`：前兩間房子只能選擇其中一間，因此取金額較大者。

### C# 程式碼

```csharp
/// <summary>
/// 使用 DP 陣列計算不偷竊相鄰房子時可以取得的最大金額。
/// </summary>
/// <param name="nums">每間房子的金額陣列。</param>
/// <returns>可以取得的最大金額。</returns>
public int Rob(int[] nums)
{
    if (nums.Length == 0)
    {
        return 0;
    }

    if (nums.Length == 1)
    {
        return nums[0];
    }

    int[] dp = new int[nums.Length];

    dp[0] = nums[0];
    dp[1] = Math.Max(nums[0], nums[1]);

    for (int i = 2; i < nums.Length; i++)
    {
        dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
    }

    return dp[nums.Length - 1];
}
```

### 複雜度分析

- 時間複雜度：`O(n)`，每間房子只計算一次。
- 空間複雜度：`O(n)`，使用 DP 陣列保存每個位置的最佳結果。

---

## 解法二：動態規劃空間優化（Rolling Variables）

### 解題思路

觀察解法一的狀態轉移方程式：

```text
dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i])
```

每次計算只會使用前兩個狀態：

- `dp[i - 1]`
- `dp[i - 2]`

因此，不需要保留整個 `dp` 陣列，只需要使用兩個變數保存前兩個狀態。

變數定義：

| 變數 | 意義 |
|---|---|
| `prev` | 前兩個位置的最佳結果 |
| `current` | 前一個位置的最佳結果 |
| `next` | 目前位置計算出的最佳結果 |

### 狀態轉移

```text
next = Math.Max(current, prev + nums[i])
```

計算完成後，將狀態往前移動：

```csharp
prev = current;
current = next;
```

必須先計算 `next`，再更新兩個變數，避免覆蓋原本需要使用的舊狀態。

### C# 程式碼

```csharp
/// <summary>
/// 使用滾動變數計算不偷竊相鄰房子時可以取得的最大金額。
/// </summary>
/// <param name="nums">每間房子的金額陣列。</param>
/// <returns>可以取得的最大金額。</returns>
public int Rob(int[] nums)
{
    if (nums.Length == 0)
    {
        return 0;
    }

    if (nums.Length == 1)
    {
        return nums[0];
    }

    int prev = nums[0];
    int current = Math.Max(nums[0], nums[1]);

    for (int i = 2; i < nums.Length; i++)
    {
        int next = Math.Max(current, prev + nums[i]);

        prev = current;
        current = next;
    }

    return current;
}
```

### 複雜度分析

- 時間複雜度：`O(n)`，只需要走訪陣列一次。
- 空間複雜度：`O(1)`，只使用固定數量的變數。

---

## 解法比較

| 解法 | 時間複雜度 | 空間複雜度 |
|---|---|---|
| DP Array | O(n) | O(n) |
| Rolling Variables | O(n) | O(1) |

## 學習重點

1. 理解 DP 狀態定義：`dp[i]` 代表考慮到目前位置時的最大金額。
2. 理解選擇與不選擇（Take or Skip）的思考方式。
3. 推導狀態轉移方程式。
4. 使用 DP 陣列保存子問題的最佳解。
5. 觀察狀態轉移只依賴前兩個狀態。
6. 使用滾動變數將空間複雜度從 O(n) 優化至 O(1)。
7. 理解狀態更新順序，避免覆蓋尚未使用的舊資料。

## 心得

這題從 DP 陣列開始，先使用 `dp[i]` 保存每個位置的最佳結果。

透過比較偷竊與不偷竊目前房子的兩種選擇，推導出狀態轉移方程式。

接著觀察到每次計算只需要前兩個狀態，因此可以使用 `prev` 和 `current` 兩個變數取代整個 DP 陣列。

這次練習不只是學會動態規劃，也學會了如何觀察狀態轉移的依賴關係，進一步進行空間優化。