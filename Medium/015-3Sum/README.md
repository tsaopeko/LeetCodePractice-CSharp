# LeetCode 15 - 3Sum

## 題目說明

給定一個整數陣列 `nums`，找出所有三個不同索引的數字組合，使得三個數字相加等於 `0`。

要求：
- 每組答案必須包含三個數字。
- 不可以重複使用相同索引。
- 回傳結果不可包含重複的組合。

### 範例

**Input**
```text
nums = [-1, 0, 1, 2, -1, -4]
```

**Output**
```text
[[-1, -1, 2], [-1, 0, 1]]
```

**說明**

符合條件的組合：

- `-1 + -1 + 2 = 0`
- `-1 + 0 + 1 = 0`

---

## 解法一：暴力解法

使用三層迴圈，分別選取三個不同索引的數字，檢查三個數字的總和是否為 `0`。

### 思考方式

1. 第一層迴圈選擇第一個數字。
2. 第二層迴圈選擇第二個數字，索引從 `i + 1` 開始。
3. 第三層迴圈選擇第三個數字，索引從 `j + 1` 開始。
4. 如果三個數字相加等於 `0`，就加入結果。

### 時間複雜度

- 三層迴圈：`O(n³)`
- 不符合題目希望的效率需求。

---

## 解法二：排序 + 雙指標（Two Pointers）

### 核心概念

先將陣列排序，再固定一個數字，利用左右雙指標尋找另外兩個數字。

假設目前固定的數字為 `nums[i]`：

```text
nums[i] + nums[j] + nums[k] = 0
```

可以轉換成：

```text
nums[j] + nums[k] = -nums[i]
```

因為陣列已經排序，所以可以根據總和決定移動哪一個指標。

### 步驟一：排序陣列

```csharp
Array.Sort(nums);
```

例如：

```text
原始陣列：
[-1, 0, 1, 2, -1, -4]

排序後：
[-4, -1, -1, 0, 1, 2]
```

### 步驟二：固定第一個數字

使用外層迴圈固定 `nums[i]`。

```csharp
for (int i = 0; i < nums.Length - 2; i++)
{
    if (i > 0 && nums[i] == nums[i - 1])
    {
        continue;
    }
}
```

因為需要另外兩個數字，所以外層迴圈最多執行到 `nums.Length - 3`。

另外，透過比較前一個數字，避免重複使用相同的固定數字。

### 步驟三：設定左右雙指標

```csharp
int j = i + 1;
int k = nums.Length - 1;
```

- `j`：從固定數字的下一個位置開始。
- `k`：從陣列最後一個位置開始。

使用 `while (j < k)` 持續尋找符合條件的組合。

### 步驟四：判斷三個數字的總和

```csharp
int sum = nums[i] + nums[j] + nums[k];

if (sum < 0)
{
    j++;
}
else if (sum > 0)
{
    k--;
}
else
{
    // 找到符合條件的組合
}
```

因為陣列已經排序：

| 條件 | 操作 | 原因 |
|---|---|---|
| `sum < 0` | `j++` | 總和太小，需要嘗試較大的數字 |
| `sum > 0` | `k--` | 總和太大，需要嘗試較小的數字 |
| `sum == 0` | 記錄答案 | 找到符合條件的組合 |

### 步驟五：記錄答案並跳過重複數字

找到符合條件的組合時：

```csharp
result.Add(new List<int> { nums[i], nums[j], nums[k] });
```

接著移動左右指標，並跳過重複的數字。

**移動 j：**

```csharp
j++;

while (j < k && nums[j] == nums[j - 1])
{
    j++;
}
```

**移動 k：**

```csharp
k--;

while (j < k && nums[k] == nums[k + 1])
{
    k--;
}
```

注意：
- `j` 移動後，與前一個位置比較。
- `k` 移動後，與後一個位置比較。
- 透過這個方式避免產生重複組合。

### 步驟六：提前結束外層迴圈

```csharp
if (nums[i] > 0)
{
    break;
}
```

因為陣列已經排序，如果固定的數字已經大於 `0`，後面的數字也一定大於或等於它。

因此三個數字相加不可能等於 `0`，可以直接結束搜尋。

---

## 完整 C# 程式碼

```csharp
/// <summary>
/// 找出整數陣列中所有總和為 0 的不重複三數組合。
/// </summary>
/// <param name="nums">輸入的整數陣列。</param>
/// <returns>所有符合條件且不重複的三數組合。</returns>
public IList<IList<int>> ThreeSum(int[] nums)
{
    List<IList<int>> result = new List<IList<int>>();

    Array.Sort(nums);

    for (int i = 0; i < nums.Length - 2; i++)
    {
        if (i > 0 && nums[i] == nums[i - 1])
        {
            continue;
        }

        if (nums[i] > 0)
        {
            break;
        }

        int j = i + 1;
        int k = nums.Length - 1;

        while (j < k)
        {
            int sum = nums[i] + nums[j] + nums[k];

            if (sum < 0)
            {
                j++;
            }
            else if (sum > 0)
            {
                k--;
            }
            else
            {
                result.Add(new List<int>
                {
                    nums[i],
                    nums[j],
                    nums[k]
                });

                j++;

                while (j < k && nums[j] == nums[j - 1])
                {
                    j++;
                }

                k--;

                while (j < k && nums[k] == nums[k + 1])
                {
                    k--;
                }
            }
        }
    }

    return result;
}
```

---

## 複雜度分析

### 時間複雜度：O(n²)

分成兩個部分：

1. 排序：`O(n log n)`
2. 外層迴圈：最多 `O(n)`，每次內層雙指標掃描最多 `O(n)`。

因此：

```text
O(n log n) + O(n × n)
= O(n²)
```

### 空間複雜度

- 雙指標只使用 `i`、`j`、`k` 等固定變數。
- 排序所需的額外空間依排序實作而定。
- 不計算存放答案的 `result`。

C# `Array.Sort` 的排序堆疊空間通常可視為 `O(log n)`。

---

## 學習重點

這題主要學習到：

1. **排序（Sorting）**：先排序，讓數字具有大小順序。
2. **雙指標（Two Pointers）**：利用左右指標降低搜尋時間。
3. **去重（Duplicate Handling）**：避免固定數字及左右指標產生重複組合。
4. **提前終止（Early Break）**：利用排序特性，當固定數字大於 `0` 時停止搜尋。
5. **時間複雜度優化**：從暴力解法的 `O(n³)` 降低至 `O(n²)`。