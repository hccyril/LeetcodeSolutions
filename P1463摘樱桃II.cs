using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, DP
    internal class P1463摘樱桃II
    {
        public int CherryPickup(int[][] grid)
        {
            int rows = grid.Length, cols = grid[0].Length;
            int[,] dp = new int[cols, cols]; // dp[i,j]: 当前行，机器人分别在i,j位置时能取得的最大值
            int[] row = grid[rows - 1];
            for (int i = 0; i < cols; ++i)
                for (int j = i; j < cols; ++j)
                    dp[i, j] = i == j ? row[i] : row[i] + row[j];
            int[,] dp1 = new int[cols, cols];
            for (int r = rows - 2; r >= 0; --r)
            {
                row = grid[r];
                for (int i = 0; i < cols; ++i) 
                    for (int j = i; j < cols; ++j)
                    {
                        dp1[i, j] = i == j ? row[i] : row[i] + row[j];
                        int max = 0;
                        for (int n1 = i == 0 ? 0 : i - 1; n1 < cols && n1 <= i + 1; ++n1) 
                            for (int n2 = j == cols - 1 ? cols - 1 : j + 1; n2 >= n1 && n2 >= j - 1; --n2)
                            {
                                int n = dp[n1, n2];
                                if (n > max) max = n;
                            }
                        dp1[i, j] += max;
                    }
                int[,] temp = dp;
                dp = dp1;
                dp1 = temp;
            }
            return dp[0, cols - 1];
        }
    }
}
