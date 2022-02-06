using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 动态规划(DP)
    internal class P0221最大正方形
    {
        public int MaximalSquare(char[][] matrix)
        {
            int max = 0;
            int[,] dp = new int[matrix.Length, matrix[0].Length]; // dp[x,y]表示(x,y)为右下角的最大正方形的边长
            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == '0') dp[i, j] = 0;
                    else if (i == 0 || j == 0) dp[i, j] = 1;
                    else dp[i, j] = 1 + Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1]);
                    if (dp[i, j] > max) max = dp[i, j];
                }
            return max * max;
        }
    }
}
