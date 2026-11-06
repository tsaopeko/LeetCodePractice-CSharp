using System.Linq.Expressions;

public class Solution {
    public bool IsValidSudoku(char[][] board) {
        HashSet<char>[,] boxes = new HashSet<char>[3, 3];
        HashSet<char>[] rows = new HashSet<char>[9];
        HashSet<char>[] cols = new HashSet<char>[9];
        for(int i = 0; i < 9; i++)
        {
            rows[i] = new HashSet<char>();
            cols[i] = new HashSet<char>();
        }
        for(int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                boxes[row, col] = new HashSet<char>();
            }
        }

        for(int row = 0; row < 9; row++)
        {          
            for(int col = 0; col < 9; col++)
            {
                char value = board[row][col];

                if(value == '.')
                {
                    continue;
                }

                int boxRow = row / 3;
                int boxCol = col / 3;
                if(boxes[boxRow, boxCol] == null)
                {
                    boxes[boxRow, boxCol] = new HashSet<char>();
                } 

                if(boxes[boxRow, boxCol].Add(value) == false ||
                 rows[row].Add(value) == false || 
                 cols[col].Add(value) == false)
                {
                    return false;
                }
            }
        }
        return true;
    }
}