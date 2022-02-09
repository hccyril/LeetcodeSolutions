using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/7
    // also 《剑指 Offer II 112. 最长递增路径》
    // 记忆化回溯
    internal class P0329矩阵中的最长递增路径
    {
        // ver1: 回溯 - TLE （结果出不来）
        // ver2: 记忆化回溯 - 171ms but wrong answer (ans: 23 exp: 140) <-- fixed

        int[,] dp;

        IEnumerable<(int ni, int nj)> FourDir(int[][] mx, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < mx.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < mx[i].Length - 1) yield return (i, j + 1);
        }

        int FindPath(int[][] mx, int i, int j)
        {
            if (dp[i, j] > 0) return dp[i, j];
            int longest = 1;
            foreach ((int ni, int nj) in FourDir(mx, i, j))
                if (mx[ni][nj] > mx[i][j])
                    longest = Math.Max(longest, 1 + FindPath(mx, ni, nj));
            return dp[i, j] = longest;
        }

        bool IsStartPoint(int[][] mx, int i, int j)
        {
            foreach ((int ni, int nj) in FourDir(mx, i, j))
                if (mx[ni][nj] < mx[i][j]) return false;
            return true;
        }

        public int LongestIncreasingPath(int[][] matrix)
        {
            dp = new int[matrix.Length, matrix[0].Length];
            int longest = 0;
            for (int i = 0; i < matrix.Length; ++i)
                for (int j = 0; j < matrix[i].Length; ++j)
                    if (IsStartPoint(matrix, i, j))
                        longest = Math.Max(longest, FindPath(matrix, i, j));
            return longest;
        }

        internal static void Run()
        {
            int[][] input = Common.ReadInput<int[][]>(329);
            var sln = new P0329矩阵中的最长递增路径();
            int ret = sln.LongestIncreasingPath(input);
            Console.WriteLine("LongestIncreasingPath=" + ret);
        }
    }
}
