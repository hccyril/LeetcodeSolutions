using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 虚拟赛
    // D题 rank: 2391
    internal class LC_WC299_20230204
    {
        #region Problem A
        public bool CheckXMatrix(int[][] grid)
        {
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                {
                    if (i == j || i + j == grid.Length - 1)
                    {
                        if (grid[i][j] == 0) return false;
                    }
                    else
                    {
                        if (grid[i][j] != 0) return false;
                    }
                }
            return true;
        }
        #endregion

        #region Problem B
        //def countHousePlacements(self, n: int) -> int:
        //    dp0 = [0] * n
        //    dp1 = [0] * n
        //    dp0[0] = dp1[0] = 1

        //    for i in range(1, n):
        //        dp0[i] = (dp0[i - 1] + dp1[i - 1]) % 1000000007
        //        dp1[i] = dp0[i - 1]

        //    x = (dp0[n - 1] + dp1[n - 1]) % 1000000007
        //    y = x * x % 1000000007

        #endregion

        #region Problem C

        // 一开始想错了方向，好久才反应过来是求最大子数组和，最后提交通过时间晚了2分钟T_T
        public int MaximumsSplicedArray(int[] nums1, int[] nums2)
        {
            int MaxSub(int[] na)
            {
                int sum = na[0], ma = 0;
                for (int i = 1; i < na.Length; ++i)
                {
                    sum = sum < 0 ? na[i] : sum + na[i];
                    ma = Math.Max(ma, sum);
                }
                return ma;
            }
            int n = nums1.Length;
            int[] d1 = Enumerable.Range(0, n).Select(i => nums1[i] - nums2[i]).ToArray();
            int[] d2 = Enumerable.Range(0, n).Select(i => nums2[i] - nums1[i]).ToArray();
            return Math.Max(nums1.Sum() + MaxSub(d2), nums2.Sum() + MaxSub(d1));
        }

        //public int MaximumsSplicedArray_2(int[] nums1, int[] nums2)
        //{

        //    int n = nums1.Length;
        //    int[] pre1 = new int[n], post1 = new int[n], post2 = new int[n], pre2 = new int[n];
        //    pre1[0] = nums1[0]; pre2[0] = nums2[0];
        //    for (int i = 1; i < n; ++i)
        //    {
        //        pre1[i] = nums1[i] + pre1[i - 1];
        //        pre2[i] = nums2[i] + pre2[i - 1];
        //    }
        //    post1[n - 1] = nums1[n - 1]; post2[n - 1] = nums2[n - 1];
        //    for (int i = n - 2; i >= 0; --i)
        //    {
        //        post1[i] = nums1[i] + post1[i + 1];
        //        post2[i] = nums2[i] + post2[i + 1];
        //    }

        //    ...
        //}
        #endregion

        #region Problem D
        public int MinimumScore(int[] nums, int[][] edges)
        {
            int mi = 2147483647; //int mi = 90000000; // WRONG!

            var dg = edges.UndirectedGraphNoLength();
            Dictionary<(int, int), int> di = new();

            // 顶点i，除去父节点p以外所有节点的异或和
            int DpDfs(int i, int p)
            {
                if (di.ContainsKey((i, p)))
                    return di[(i, p)];
                int sm = nums[i];
                foreach (int j in dg[i])
                {
                    if (j != p)
                    {
                        sm ^= DpDfs(j, i);
                    }
                }
                return di[(i, p)] = sm;
            }

            foreach (var e in edges)
            {
                DpDfs(e[0], e[1]);
                DpDfs(e[1], e[0]);
            }

            void PostOrder(int i, int p, int x, int y)
            {
                foreach (int j in dg[i])
                    if (j != p)
                    {
                        PostOrder(j, i, x, y);
                        int xxx = di[(j, i)], xx = y ^ xxx;
                        int high = Math.Max(x, Math.Max(xx, xxx)), low = Math.Min(x, Math.Min(xx, xxx));
                        mi = Math.Min(mi, high - low);
                    }
            }

            // enum all edges
            foreach (var e in edges)
            {
                int a = e[0], b = e[1];
                foreach (var _ in Enumerable.Range(0, 2))
                {
                    (a, b) = (b, a);
                    int x = di[(a, b)], y = di[(b, a)];
                    PostOrder(b, a, x, y);
                    //mi = Math.Min(mi, PostOrder(b, a, x, y));
                }
            }

            return mi;
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
            LC_WC299_20230204 sln = new();

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
