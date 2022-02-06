using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 
     * 576. Out of Boundary Paths

There is an m x n grid with a ball. The ball is initially at the position [startRow, startColumn]. You are allowed to move the ball to one of the four adjacent four cells in the grid (possibly out of the grid crossing the grid boundary). You can apply at most maxMove moves to the ball.

Given the five integers m, n, maxMove, startRow, startColumn, return the number of paths to move the ball out of the grid boundary. Since the answer can be very large, return it modulo 109 + 7.
    https://leetcode.com/problems/out-of-boundary-paths/
     * */
    class P0576出界的路径数
    {
        public int FindPaths(int m, int n, int maxMove, int startRow, int startColumn)
        {
            const int MODULO = 1000000007;
            int[,] dp = new int[m, n], dp0 = new int[m, n], dpTemp = null;
            int result = 0;
            for (int move = 1; move <= maxMove; ++move)
            {
                for (int i = 0; i < m; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        if (move == 1)
                        {
                            if (i == 0) dp[i, j]++;
                            if (i == m - 1) dp[i, j]++;
                            if (j == 0) dp[i, j]++;
                            if (j == n - 1) dp[i, j]++;
                        } 
                        else
                        {
                            int sum = 0;
                            if (i > 0) sum += dp0[i - 1, j]; if (sum > MODULO) sum -= MODULO;
                            if (i < m - 1) sum += dp0[i + 1, j]; if (sum > MODULO) sum -= MODULO;
                            if (j > 0) sum += dp0[i, j - 1]; if (sum > MODULO) sum -= MODULO;
                            if (j < n - 1) sum += dp0[i, j + 1]; if (sum > MODULO) sum -= MODULO;
                            dp[i, j] = sum;
                        }
                    }
                }
                dpTemp = dp0; dp0 = dp; dp = dpTemp;
                result += dp0[startRow, startColumn]; if (result > MODULO) result -= MODULO;
            }
            return result;
        }

        public static void Run()
        {
            Console.WriteLine(new P0576出界的路径数().FindPaths(1, 3, 3, 0, 1)); // 12
            Console.WriteLine(new P0576出界的路径数().FindPaths(8, 50, 23, 5, 26)); // 914783380
        }
    }
}
