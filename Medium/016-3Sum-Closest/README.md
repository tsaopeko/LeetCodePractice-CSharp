# LeetCode 16 - 3Sum Closest

## 題目說明

給定一個整數陣列 `nums` 和一個整數 `target`，從陣列中選出三個不同位置的數字，使這三個數字的總和最接近 `target`。

回傳最接近 `target` 的三數總和。

題目保證只有一個最接近的答案。

## 範例

**Example 1**

```text
Input: nums = [-1, 2, 1, -4], target = 1
Output: 2

Explanation:
-1 + 2 + 1 = 2
2 與 target = 1 的差距為 1，是最接近的總和。
```

**Example 2**

```text
Input: nums = [0, 0, 0], target = 1
Output: 0
```

---

## 解題方法：排序 + 雙指標（Sorting + Two Pointers）

### 1. 排序陣列

首先使用 `Array.Sort()` 將陣列由小到大排序。

```csharp
Array.Sort(nums);
```

排序後，可以利用雙指標的方式有效率地調整三數總和。

### 2. 初始化最接近的總和

先使用陣列前三個數字的總和作為初始答案。

```csharp
int closestSum = nums[0] + nums[1] + nums[2];
```

這樣可以確保一開始就有一組合法的三數總和，不需要額外設定特殊初始值。

### 3. 外層迴圈固定第一個數字

使用 `for` 迴圈固定第一個數字 `nums[i]`。

```csharp
for (int i = 0; i < nums.Length - 2; i++)
```

因為每次需要選擇三個不同位置的數字，所以 `i` 最多只能到 `nums.Length - 3`。

### 4. 使用雙指標尋找另外兩個數字

固定 `nums[i]` 後：

```csharp
int j = i + 1;
int k = nums.Length - 1;
```

- `j`：從固定數字的下一個位置開始。
- `k`：從陣列最後一個位置開始。

當 `j < k` 時，持續計算三個數字的總和。

```csharp
int sum = nums[i] + nums[j] + nums[k];
```

### 5. 更新最接近的總和

使用 `Math.Abs()` 計算目前總和與目標值的絕對差距。

```csharp
if (Math.Abs(sum - target) < Math.Abs(closestSum - target))
{
    closestSum = sum;
}
```

如果目前總和與 `target` 的差距更小，就更新 `closestSum`。

### 6. 移動雙指標

根據目前總和與目標值的大小決定移動方向。

```csharp
if (sum < target)
{
    j++;
}
else if (sum > target)
{
    k--;
}
else
{
    return sum;
}
```

- `sum < target`：總和太小，移動 `j`，嘗試增加總和。
- `sum > target`：總和太大，移動 `k`，嘗試減少總和。
- `sum == target`：已經找到完全符合的答案，直接回傳。

當所有組合都檢查完畢後，回傳 `closestSum`。

---

## 完整 C# 程式碼

```csharp
public class Solution
{
    /// <summary>
    /// 找出陣列中三個數字的總和，使其最接近指定目標值。
    /// </summary>
    /// <param name="nums">輸入的整數陣列。</param>
    /// <param name="target">目標總和。</param>
    /// <returns>最接近目標值的三數總和。</returns>
    public int ThreeSumClosest(int[] nums, int target)
    {
        Array.Sort(nums);

        int closestSum = nums[0] + nums[1] + nums[2];

        for (int i = 0; i < nums.Length - 2; i++)
        {
            int j = i + 1;
            int k = nums.Length - 1;

            while (j < k)
            {
                int sum = nums[i] + nums[j] + nums[k];

                if (Math.Abs(sum - target) < Math.Abs(closestSum - target))
                {
                    closestSum = sum;
                }

                if (sum < target)
                {
                    j++;
                }
                else if (sum > target)
                {
                    k--;
                }
                else
                {
                    return sum;
                }
            }
        }

        return closestSum;
    }
}
```

---

## 時間與空間複雜度分析

### 時間複雜度：O(n²)

| 操作 | 時間複雜度 |
|---|---|
| 陣列排序 | O(n log n) |
| 外層 for 迴圈 | O(n) |
| 內層雙指標 | O(n) |
| 整體 | O(n²) |

外層迴圈最多執行 n 次，每次內層雙指標最多移動 n 次，因此整體時間複雜度為 O(n²)。

### 空間複雜度：O(1) 額外輔助空間

演算法本身只使用 `i`、`j`、`k`、`sum` 和 `closestSum` 等固定數量的變數，不需要額外建立資料結構。

不過，`Array.Sort()` 的內部排序實作可能使用額外堆疊空間，因此這裡的 O(1) 指的是演算法本身的額外輔助空間，不包含排序實作。

---

## 學習重點

1. 熟悉排序後搭配雙指標的解題方式。
2. 使用 `Math.Abs()` 計算兩個數值之間的絕對差距。
3. 使用 `closestSum` 持續保存目前最接近目標值的結果。
4. 根據總和與目標值的大小，決定雙指標移動方向。
5. 當總和完全等於目標值時，可以直接回傳。
6. 理解不同題目即使使用相同的演算法架構，也可能有不同的資料處理需求。

## 與 LeetCode 15 - 3Sum 的差異

| 比較項目 | 3Sum | 3Sum Closest |
|---|---|---|
| 解題方式 | 排序 + 雙指標 | 排序 + 雙指標 |
| 固定第一個數字 | 是 | 是 |
| 去除重複組合 | 需要 | 不需要 |
| 結果 | 所有符合條件的三數組合 | 最接近目標值的總和 |
| 時間複雜度 | O(n²) | O(n²) |

3Sum 需要回傳所有不重複的三數組合，因此必須處理重複數字。

3Sum Closest 只需要回傳最接近目標值的總和，即使重複檢查相同的數字組合，也不會影響答案，因此不需要額外的去重複邏輯。

---

## 總結

這題延續了 LeetCode 15 - 3Sum 的排序與雙指標技巧。

主要差異在於，3Sum 是尋找符合特定總和的組合，而 3Sum Closest 則是持續比較差距，保留最接近目標值的結果。

透過這題，可以進一步熟悉雙指標在不同問題中的應用。