using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2023/12/26 Daily
// 回溯 / DFS
internal class P1349参加考试的最大学生数
{
    // 记忆化回溯 - AC
    public int MaxStudents(char[][] seats)
    {
        int m = seats.Length, n = seats[0].Length;

        Dictionary<(int, int), int> dp = new(); // [第i行 且上一行方案为jm]的最优解
        // 返回(i, j) 到 (m-1, n-1)范围内最大的座位数
        int Dfs(int i, int j)
        {
            if (i == m) return 0;
            int ans = 0, bm = 0;
            if (i > 0 && j == 0)
            {
                for (int k = 0; k < n; ++k)
                    if (seats[i - 1][k] == 'h')
                        bm |= 1 << k;
                if (dp.TryGetValue((i, bm), out int a))
                    return a;
            }
            try
            {
                int ni = i, nj = j + 1;
                if (nj == n) { ni = i + 1; nj = 0; }
                if (seats[i][j] != '.') return ans = Dfs(ni, nj);
                if (j > 0 && seats[i][j - 1] == 'h') return ans = Dfs(ni, nj);
                if (i > 0 && j > 0 && seats[i - 1][j - 1] == 'h') return ans = Dfs(ni, nj);
                if (i > 0 && j < n - 1 && seats[i - 1][j + 1] == 'h') return ans = Dfs(ni, nj);

                seats[i][j] = 'h';
                int c1 = 1 + Dfs(ni, nj);
                seats[i][j] = '.';
                int c2 = Dfs(ni, nj);
                return ans = Math.Max(c1, c2);
            }
            finally
            {
                if (i > 0 && j == 0)
                    dp[(i, bm)] = ans;
            }
        }

        return Dfs(0, 0);
    }

    // ver1 DFS - 超时，需要优化
    public int MaxStudents_TLE(char[][] seats)
    {
        int m = seats.Length, n = seats[0].Length, maxs = 0, h = 0;
        int Dfs(int i, int j)
        {
            if (i == m) return h;
            int ni = i, nj = j + 1;
            if (nj == n) { ni = i + 1; nj = 0; }
            if (seats[i][j] != '.') return Dfs(ni, nj);
            if (j > 0 && seats[i][j - 1] == 'h') return Dfs(ni, nj);
            if (i > 0 && j > 0 && seats[i - 1][j - 1] == 'h') return Dfs(ni, nj);
            if (i > 0 && j < n - 1 && seats[i - 1][j + 1] == 'h') return Dfs(ni, nj);

            seats[i][j] = 'h';
            maxs = Math.Max(maxs, ++h);
            Dfs(ni, nj);
            seats[i][j] = '.';
            --h;
            return Dfs(ni, nj);
        }

        Dfs(0, 0);
        return maxs;
    }
}
