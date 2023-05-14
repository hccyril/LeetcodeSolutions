using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 现场赛，最后一刻才AC
    // 266 / 2631	呱呱编程实验室 	17	1:44:37	 0:01:55	 0:05:25	 1:24:37 * 1	 0:58:21 * 3
    // 2023/2/11 更新：最后一题其实是错的，排名下降了一倍：
    // 519 / 2631	呱呱编程实验室 	12	1:29:37	 0:01:55	 0:05:25	 1:24:37 * 1	
    internal class LC_BiC097_20230204
    {
        #region Problem A
        public int[] SeparateDigits(int[] nums)
        {
            List<int> li = new();
            foreach (int x in nums)
            {
                foreach (var c in x.ToString())
                {
                    li.Add(c - '0');
                }
            }
            return li.ToArray();
        }
        #endregion

        #region Problem B
        /**
            def maxCount(self, banned: List[int], n: int, maxSum: int) -> int:
                bs = set(banned)
                sm, cnt = 0, 0
                for i in range(1, n + 1):
                    if i in bs:
                        pass
                    elif sm + i <= maxSum:
                        sm += i
                        cnt += 1
                    else:
                        break
                return cnt
         * */
        #endregion

        #region Problem C
        // 一开始以为是贪心法，后来发现不对，好在来得及改成线性规划
        public int MaximizeWin(int[] prizePositions, int k)
        {
            Dictionary<int, int> di = new();
            foreach (int p in prizePositions)
            {
                if (!di.ContainsKey(p)) di[p] = 0;
                di[p]++;
            }
            var sort = di.Select(kv => (kv.Key, kv.Value)).OrderBy(t => t.Key).ToArray();
            int n = sort.Length;

            int i = 0, j = 0, ans = 0, sum = 0, ai = 0, aj = 0;
            int[] dpL = new int[n];

            for (; j < sort.Length; ++j)
            {
                sum += sort[j].Value;
                while (i < j && sort[i].Key < sort[j].Key - k)
                {
                    sum -= sort[i].Value;
                    ++i;
                }
                if (sum > ans)
                {
                    ans = sum;
                    ai = i; aj = j;
                }
                dpL[j] = ans;
            }

            int ans2 = 0;
            sum = 0;
            int[] dpR = new int[n];
            for (i = j = n - 1; j >= 0; --j)
            {
                sum += sort[j].Value;
                while (i > j && sort[i].Key > sort[j].Key + k)
                {
                    sum -= sort[i].Value;
                    --i;
                }
                if (sum > ans2)
                {
                    ans2 = sum;
                    ai = i; aj = j;
                }
                dpR[j] = ans2;
            }

            ans = Math.Max(ans, ans2);
            for (i = 1; i < n; ++i)
            {
                ans = Math.Max(ans, dpL[i - 1] + dpR[i]);
            }
            return ans;
        }
        #endregion

        #region Problem D
        // 居然出了一题Tarjan，幸亏找回了代码在哪里
        // 更新：把图改成无向图然后Tarjan是错的，反例：[[1,1,0,1,1,1],[1,1,1,1,0,1],[1,1,1,1,1,1]] (ans should be true)
        
        // 参考题解后的解法：走上路和下路，看两条路是否有相交点
        public bool IsPossibleToCutPath(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;

            // 特殊处理类似[[1]]或者[[1,1]]这样的特例
            if (m == 1 || n == 1) return m + n > 3;

            // 上路: 先右后下
            HashSet<(int, int)> hs = new();
            bool Dfs1(int i, int j)
            {
                if (i == m - 1 && j == n - 1) return true;
                foreach ((int ni, int nj) in new (int, int)[] { (i, j + 1), (i + 1, j) })
                    if (ni < m && nj < n && grid[ni][nj] == 1)
                    {
                        if (Dfs1(ni, nj))
                        {
                            hs.Add((i, j));
                            return true;
                        }
                    }
                return false;
            }

            if (!Dfs1(0, 1)) return true; // 如果上路走不通，那(1,0)便是割点

            // 下路: 先下后右
            bool Dfs2(int i, int j)
            {
                if (hs.Contains((i, j))) throw new Exception();
                foreach ((int ni, int nj) in new (int, int)[] { (i + 1, j), (i, j + 1) })
                    if (ni < m && nj < n && grid[ni][nj] == 1)
                    {
                        if (ni == m - 1 && nj == n - 1) return true;
                        if (Dfs2(ni, nj)) return true;
                    }
                return false;
            }

            try
            {
                return !Dfs2(1, 0);
            }
            catch
            {
                return true;
            }
        }
        public bool IsPossibleToCutPath_WA1(int[][] grid)
        {
            dfn = new(); low = new();
            Dfs(grid, 0, 0);
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[0].Length; ++j)
                {
                    if (i == 0 && j == 0 || i == grid.Length - 1 && j == grid[0].Length - 1)
                        continue;
                    if (grid[i][j] > 2)
                        return true;
                }

                    //if ((i > 0 || j > 0) && (i < grid.Length - 1 || j < grid.Length - 1) && grid[i][j] > 2)
                    //    return true;
            return false;
        }

        Dictionary<(int, int), int> dfn, low;
        int timestamp = 0;

        int Dfs(int[][] grid, int i, int j)
        {
            grid[i][j] = 2;
            low[(i, j)] = dfn[(i, j)] = ++timestamp;
            int branch = 0;
            foreach ((int ni, int nj) in grid.FourDir(i, j))
                //new (int, int)[] { (i + 1, j), (i, j + 1) })
            {
                if (ni < grid.Length && nj < grid[0].Length && grid[ni][nj] == 1)
                {
                    ++branch;
                    int low_ret = Dfs(grid, ni, nj);
                    // tarjan定义：当子节点的low值比当前节点要大，表示沿着子节点的路径回不到祖先，当前节点为割点
                    // 注意：这个判断不适用于根节点（root i.e. dfn==1)
                    if (dfn[(i, j)] > 1 && low_ret >= dfn[(i, j)]) grid[i][j] = 3;

                    // 回溯时更新low的值
                    low[(i, j)] = Math.Min(low[(i, j)], low_ret);
                }
                else if (ni < grid.Length && nj < grid[0].Length && grid[ni][nj] > 1)
                {
                    // 目标节点已经访问过，更新low值
                    low[(i, j)] = Math.Min(low[(i, j)], dfn[(ni, nj)]);
                }
            }
            // 对于根节点，是否为割点的判断方式是有没有多个回溯分支
            if (dfn[(i, j)] == 1 && branch > 1)
                grid[i][j] = 3;

            return low[(i, j)];
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
            LC_BiC097_20230204 sln = new();

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
            // 一直错，到最后用了7个case才过
            // [[1,1,1],[1,0,0],[1,1,1]] true
            // [[1,1,1],[1,0,1],[1,1,1]] false
            // [[1,1,0,1,1,1],[1,1,1,1,0,1],[1,1,1,1,1,1]] true
            // [[1,1]] false
            // [[1,1,1]] true
            // [[1,1,1],[1,1,0],[1,1,1]] true
            // [[1]] false


            //int[] a1 = { 1, 1, 1 }, a2 = { 1, 0, 1 }, a3 = { 1, 1, 1 };
            //int[][] aa = { a1, a2, a3 };
            int[] a = { 1, 1, 1, 1, 1 };
            int[][] aa = { a };
            bool ret = IsPossibleToCutPath(aa);
            Console.WriteLine("ret=" + ret);
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
