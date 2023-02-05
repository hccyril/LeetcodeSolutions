using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 20230204模拟赛-对应2325-2328
    // https://leetcode.cn/contest/weekly-contest-300/
    // D题rank 2001
    // # 用时0:47:13，排名204 / 6792 详细：18	0:47:13	0:07:13	0:18:35	0:47:13	0:37:00
    internal class LC_WC300_20230204
    {
        #region Problem A
        public string DecodeMessage(string key, string message)
        {
            Dictionary<char, char> di = new();
            for (char c = 'a'; c <= 'z'; ++c)
            {
                di[c] = ' ';
            }
            di[' '] = '_';
            char t = 'a';
            foreach (var c in key)
            {
                if (di[c] == ' ')
                    di[c] = t++;
            }
            di[' '] = ' ';
            return string.Join("", message.Select(c => di[c]));
        }
        #endregion

        #region Problem B
        //def spiralMatrix(self, m: int, n: int, head: Optional[ListNode]) -> List[List[int]]:
        //    mx = [[-1] * n for _ in range(m)]
        //    dx, di = 0, [[0, 1], [1, 0], [0, -1], [-1, 0]]
        //    cur, i, j = head, 0, 0
        //    while cur:
        //        mx[i][j] = cur.val
        //        ni, nj = i + di[dx][0], j + di[dx][1]
        //        if ni >= 0 and ni < m and nj >= 0 and nj < n and mx[ni][nj] < 0:
        //            pass
        //        else:
        //            dx = (dx + 1) % 4
        //        i, j = i + di[dx][0], j + di[dx][1]
        //        cur = cur.next
        //    return mx
        #endregion

        #region Problem C
        //def peopleAwareOfSecret(self, n: int, delay: int, forget: int) -> int:
        //    dp = [0] * n
        //    dp[0] = 1
        //    for i in range(1, n):
        //        start = i - forget
        //        if start >= 0:
        //            dp[start] = 0
        //            start += 1
        //        else:
        //            start = 0
        //        end = i - delay + 1
        //        if end > start:
        //            for j in range(start, end):
        //                dp[i] += dp[j]
        //    return sum(dp) % 1000000007
        #endregion

        #region Problem D
        //def countPaths(self, grid: List[List[int]]) -> int:
        //    m, n = len(grid), len(grid[0])
        //    li = [[grid[i][j], i, j] for i, j in product(range(len(grid)), range(len(grid[0])))]
        //    li.sort()
        //    dp = [[1] * n for _ in range(m)]
        //    for x, i, j in li:
        //        for pi, pj in [[i - 1, j], [i, j - 1], [i + 1, j], [i, j + 1]]:
        //            if pi >= 0 and pi < m and pj >= 0 and pj < n and grid[pi][pj] < x:
        //                dp[i][j] += dp[pi][pj]
        //    return sum(sum(rw) for rw in dp) % 1000000007
        #endregion

        #region Problem E
        public int SolveE(int x)
        {
            return x;
        }
        #endregion

        #region Run Test
        internal static int Run()
        {
            char p = 'D';
            LC_WC300_20230204 sln = new();

            return p switch
            {
                'A' => sln.RunTestA(),
                'B' => sln.RunTestB(),
                'C' => sln.RunTestC(),
                'D' => sln.RunTestD(),
                'E' => sln.RunTestE(),
                _ => -1
            };
        }

        int RunTestA()
        {
            return 0;
        }

        int RunTestB()
        {
            return 0;
        }

        int RunTestC()
        {
            return 0;
        }

        int RunTestD()
        {
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
