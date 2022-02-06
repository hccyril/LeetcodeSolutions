using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 模拟
    internal class P1706球会落何处
    {
        // dir: 0: 从上面掉下来；1：从左边进来；2：从右边进来
        int Roll(int[][] grid, int row, int col, int dir)
        {
            if (row >= grid.Length) return col;
            else if (col < 0 || col >= grid[0].Length) return -1;
            if (grid[row][col] == 1)
                return dir switch
                {
                    0 => Roll(grid, row, col + 1, 1),
                    1 => Roll(grid, row + 1, col, 0),
                    _ => -1,
                };
            else
                return dir switch
                {
                    0 => Roll(grid, row, col - 1, 2),
                    2 => Roll(grid, row + 1, col, 0),
                    _ => -1,
                };
        }
        public int[] FindBall(int[][] grid)
        {
            int[] ans = new int[grid[0].Length];
            for (int col = 0; col < grid[0].Length; ++col)
                ans[col] = Roll(grid, 0, col, 0);
            return ans;
        }
    }
}
