using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 这次比赛巨简单，34分钟就全做完了，3,4题都是中等题
    internal class LC_WC335_20230514
    {
        #region Problem A
        /*
        class Solution:
            def circularGameLosers(self, n: int, k: int) -> List[int]:
                cn = Counter()
                cn[0] = 1
                p, i = 0, 0
                while True:
                    p += k
                    i = (i + p) % n
                    cn[i] += 1
                    if cn[i] > 1:
                        break
                return [x + 1 for x in range(n) if not cn[x]]
         * */
        #endregion

        #region Problem B
        /*
         * 以下为比赛提交代码
         * 然后后来想到没必要验证两次，如果初始0有解，初始1必定有解
         * 然后后来看到别人题解一行搞定（只需判断derived所有数异或和为0）
        class Solution:
            def doesValidArrayExist(self, derived: List[int]) -> bool:
                n = len(derived)
                a = [0] * n
                def dfs(i):
                    nonlocal a, derived
                    if i == n - 1:
                        return a[-1] ^ a[0] == derived[-1]
                    a[i + 1] = derived[i] ^ a[i]
                    return dfs(i + 1)
                a[0] = 0
                if dfs(0):
                    return True
                a[0] = 1
                return dfs(0)
         * */
        #endregion

        #region Problem C
        /*
        class Solution:
            def maxMoves(self, grid: List[List[int]]) -> int:
                ans, m, n = 0, len(grid), len(grid[0])
                dp = [[0] * n for _ in range(m)]
                for j in range(n - 2, -1, -1):
                    for i in range(m):
                        for d in [-1, 0, 1]:
                            ni, nj = i + d, j + 1
                            if 0 <= ni < m and grid[ni][nj] > grid[i][j]:
                                dp[i][j] = max(dp[i][j], dp[ni][nj] + 1)
                                if j == 0:
                                    ans = max(ans, dp[i][j])
                return ans
         * */
        #endregion

        #region Problem D
        public int CountCompleteComponents(int n, int[][] edges)
        {
            UnionFind uni = new(n);
            foreach (var ed in edges)
            {
                int a = ed[0], b = ed[1];
                uni.Union(a, b);
            }
            var di = Enumerable.Range(0, n).Select(i => uni.Find(i)).GroupBy(x => x).ToDictionary(g => g.Key, g => (g.Count() * (g.Count() - 1)) >> 1);
            foreach (var ed in edges)
            {
                int a = ed[0];
                di[uni.Find(a)]--;
            }

            return di.Count(p => p.Value == 0);
        }
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
            LC_WC335_20230514 sln = new();

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
