using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/6/20 Daily
    // dijkstra but TLE
    // 2023/7/8 再次尝试求解（还差动态规划版本未完成）
    internal class P1595连通两组点的最小成本
    {
        // ver3 - 应用排列组合：因为(1,3)和(3,1）是一样的，所以维护最后一个选择的连接，下一个连接不能往前反选
        // 实测结果：625ms
        // 提交到美版网站通过（3202ms），国内平台总时间超时
        public int ConnectTwoGroups(IList<IList<int>> cost)
        {
            int m = cost.Count, n = cost[0].Count;
            List<(int, int)> li = new();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    li.Add((1 << i | 1 << m + j, cost[i][j]));
            int mmp = (1 << m) - 1;
            int fm = (1 << m + n) - 1;
            SHeap<(int, int), int> hp = new((a, b) => a < b, true);
            hp.Add((0, 0), 0);
            while (hp.Any())
            {
                ((int bm, int i), int p) = hp.Pop();
                if (bm == fm) return p;
                else if (i == li.Count) continue;

                for (; i < li.Count; ++i) // 添加规则1：必须从上一个选择开始往下选
                {
                    (int b, int c) = li[i];
                    int bn = b | bm;
                    // 添加规则2：既然不能返回来选，那漏掉是不行的
                    if ((bn + 1 & bn & mmp) != 0) // 这里位运算的意思是，后m位末尾必须是连续的1，否则break
                        break;
                    if (bn != bm)
                        hp.Add((bn, i + 1), p + c);
                }
            }
            return -1;
        }

        // 动态规划 - 看题解后自己写
        // 枚举i，枚举j的所有子集，O(2^2 * 12 * 12) = 589824
        // 实测：159ms，对比dijkstra 3202ms，确实快很多（但想不明白为什么）
        public int ConnectTwoGroups_DP(IList<IList<int>> cost)
        {
            int m = cost.Count, n = cost[0].Count;
            int[] dp0 = new int[1 << n], dp1 = new int[1 << n];
            Array.Fill(dp1, -1);
            dp1[0] = 0;
            for (int i = 0; i < m; ++i)
            {
                // step1 在 dp0的基础上加一条(i,x)得到dp1
                (dp0, dp1) = (dp1, dp0);
                Array.Fill(dp1, -1);
                for (int jm = 0; jm < (1 << n); ++jm)
                    if (dp0[jm] >= 0)
                        for (int j = 0; j < n; ++j)
                        {
                            int jn = jm | 1 << j;
                            int p = dp0[jm] + cost[i][j];
                            dp1[jn] = dp1[jn] < 0 ? p : Math.Min(dp1[jn], p);
                        }

                // step2 在dp1的基础上加更多的(i, x)边是否能有更优解
                for (int jm = 0; jm < (1 << n); ++jm)
                    if (dp1[jm] >= 0)
                        for (int j = 0; j < n; ++j)
                            if ((jm & 1 << j) == 0)
                            {
                                int jn = jm | 1 << j;
                                int p = dp1[jm] + cost[i][j];
                                dp1[jn] = dp1[jn] < 0 ? p : Math.Min(dp1[jn], p);
                            }
            }
            return dp1[(1 << n) - 1];
        }

        // WA again - Bak
        //public int ConnectTwoGroups_DP(IList<IList<int>> cost)
        //{
        //    int m = cost.Count, n = cost[0].Count;
        //    int[] dp = new int[1 << n];
        //    Array.Fill(dp, -1);
        //    dp[0] = 0;
        //    for (int i = 0; i < m; ++i)
        //        for (int j = 0; j < n; ++j)
        //        {
        //            for (int jm = 0; jm < (1 << n); ++jm)
        //                if (dp[jm] >= 0)
        //                {
        //                    int jn = jm | 1 << j;
        //                    int p = dp[jm] + cost[i][j];
        //                    dp[jn] = dp[jn] < 0 ? p : Math.Min(dp[jn], p);
        //                }
        //        }
        //    return dp[(1 << n) - 1];
        //}

        // WA - bak

        //public int ConnectTwoGroups_DP(IList<IList<int>> cost)
        //{
        //    int m = cost.Count, n = cost[0].Count;
        //    int[] dp0 = new int[1 << n], dp1 = new int[1 << n];
        //    Array.Fill(dp1, -1);
        //    dp1[0] = 0;
        //    for (int i = 0; i < m; ++i)
        //    {
        //        Console.WriteLine("i=" + i);
        //        (dp0, dp1) = (dp1, dp0);
        //        Array.Fill(dp1, -1);
        //        for (int jm = 0; jm < (1 << n); ++jm)
        //            if (dp0[jm] >= 0)
        //                for (int j = 0; j < n; ++j)
        //                {
        //                    int jn = jm | 1 << j;
        //                    int p = dp0[jm] + cost[i][j];
        //                    dp1[jn] = dp1[jn] < 0 ? p : Math.Min(dp1[jn], p);
        //                }
        //        for (int j = 0; j < (1 << n); ++j)
        //            if (dp1[j] >= 0)
        //                Console.Write("   {0}:{1}", j, dp1[j]);
        //        Console.WriteLine();
        //    }
        //    return dp1[(1 << n) - 1];
        //}



        // ver2 - 尝试优化 - 添加BitSetTree去重：优化到大约700ms
        // 提交后后面的用例还是超时
        public int ConnectTwoGroups_2(IList<IList<int>> cost)
        {
            int m = cost.Count, n = cost[0].Count;
            List<(int, int)> li = new();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    li.Add((1 << i | 1 << m + j, cost[i][j]));

            BitSetTree bs = new(m + n);

            int fm = (1 << m + n) - 1;
            SHeap<int, int> hp = new((a, b) => a < b, true);
            hp.Add(0, 0);
            while (hp.Any())
            {
                ++c1; // DEBUG
                (int bm, int p) = hp.Pop();
                if (bm == fm) return p;
                else if (bs[bm]) continue;
                else bs[bm] = true;
                ++c2; // DEBUG

                //// 进一步优化: 8891ms -> 1297ms BUT WA -_-|||
                //for (int i = li.Count - 1; i >= 0; --i)
                //{
                //    (int b, int c) = li[i];
                //    if (bs[b]) li.RemoveAt(i);
                //    else if ((b | bm) != bm)
                //        hp.Add(b | bm, p + c);
                //}
                foreach ((int b, int c) in li)
                    if ((b | bm) != bm)
                        hp.Add(b | bm, p + c);
            }
            return -1;
        }
        internal static int c1 = 0, c2 = 0;

        // ver1 - dijkstra - 超时(3657ms)
        public int ConnectTwoGroups_1(IList<IList<int>> cost)
        {
            int m = cost.Count, n = cost[0].Count;
            List<(int, int)> li = new();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    li.Add((1 << i | 1 << m + j, cost[i][j]));
            int fm = (1 << m + n) - 1;
            SHeap<int, int> hp = new((a, b) => a < b, true);
            hp.Add(0, 0);
            while (hp.Any())
            {
                (int bm, int p) = hp.Pop();
                if (bm == fm) return p;
                foreach ((int b, int c) in li)
                    if ((b | bm) != bm)
                        hp.Add(b | bm, p + c);
            }
            return -1;
        }

        internal static void Run()
        {
            // test case #0: ans=4
            var raw = "[[1,3,5],[4,1,1],[1,5,3]]";

            // test case #1: ans=102; 36xx -> 7xx ms
            //var raw = "[[32,11,79,32,75,39,27,67],[53,8,1,70,42,68,79,92],[89,0,64,57,4,15,55,59],[68,4,75,29,5,20,89,95],[70,82,44,6,63,41,92,67],[23,96,34,13,98,72,92,35],[93,9,63,42,65,47,50,38],[86,89,5,32,55,53,29,20],[77,33,79,64,0,44,82,6],[69,55,18,12,89,54,97,10],[56,91,30,2,30,83,67,60]]";

            // test case #2: ans=120; 8891ms
            //var raw = "[[66,78,9,28,3,70,92,79,18,63,80],[56,7,12,6,1,90,1,34,21,36,62],[4,68,28,93,17,38,72,96,26,89,63],[11,94,47,60,46,36,0,69,52,47,35],[18,76,87,51,37,9,38,60,45,98,9],[64,78,41,95,59,23,90,9,31,10,23],[25,52,84,48,44,69,3,9,50,13,15],[33,90,38,6,65,9,11,99,67,64,61],[29,30,79,46,78,74,89,80,98,92,89],[41,18,58,65,25,18,91,21,11,17,8],[41,39,38,47,100,55,96,29,62,38,94]]";

            var a = raw.ToTestInput<int[][]>();
            var sln = new P1595连通两组点的最小成本();
            var ans = sln.ConnectTwoGroups_DP(a);
            Console.WriteLine("ans=" + ans);
            Console.WriteLine("c1={0} c2={1}", c1, c2);
        }
    }
}
