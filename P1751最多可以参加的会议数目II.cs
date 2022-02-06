using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, DP, P1235进阶版
    class P1751最多可以参加的会议数目II
    {
        void 说明()
        {
            var 相关题1 = new P1235规划兼职工作();
            var 相关题2 = new P1353最多可以参加的会议数目();
        }

        public int MaxValue(int[][] events, int k)
        {
            int n = events.Length;
            var arr = Enumerable.Range(0, n).Select(i => new
            {
                start = events[i][0],
                end = events[i][1],
                val = events[i][2]
            }).OrderBy(t => t.end).ThenBy(t => t.start).ToArray();
            int[] endTime = arr.Select(t => t.end).ToArray();
            int[] startTime = new int[n];
            for (int i = 0; i < n; ++i)
            {
                int index = Array.BinarySearch(endTime, 0, i, arr[i].start);
#if DEBUGxx     // 如果使用以下这句就会WA，对于二分法中搜索有重复元素时要注意，并不一定返回第一个结果
                int i2 = Array.BinarySearch(endTime, arr[i].start);
                if (i2 != index)
                {
                    Console.WriteLine("wrong");
                }
#endif
                startTime[i] = index < 0 ? index = -1 - index : index;
            }
            int[,] dp = new int[n, k];
            for (int i = 0; i < n; ++i)
                dp[i, 0] = Math.Max(i > 0 ? dp[i - 1, 0] : 0, arr[i].val);


            for (int t = 1; t < k; ++t)
            {
                for (int i = 0; i < n; ++i)
                {
                    dp[i, t] = Math.Max(dp[i, t - 1], i > 0 ? dp[i - 1, t] : 0);
                    int index = startTime[i];
                    if (index > 0) dp[i, t] = Math.Max(dp[i,t], arr[i].val + dp[index - 1, t - 1]);
                }
            }
#if DEBUGxx
            for (int t = 0; t < k; ++t)
            {
                for (int i = 0; i < n; ++i)
                    Console.Write(" " + dp[i, t]);
                Console.WriteLine();
            }
#endif
            return dp[n - 1, k - 1];
        }

        internal static void Run()
        {
            var input = new int[][] { new int[] { 41, 57, 75 }, new int[] { 35, 100, 50 }, new int[] { 52, 66, 10 }, new int[] { 57, 96, 17 }, new int[] { 66, 99, 40 }, new int[] { 12, 73, 51 }, new int[] { 42, 65, 94 }, new int[] { 79, 94, 7 }, new int[] { 22, 64, 84 }, new int[] { 54, 54, 65 }, new int[] { 61, 64, 49 }, new int[] { 5, 12, 68 }, new int[] { 57, 89, 25 }, new int[] { 40, 79, 93 }, new int[] { 42, 92, 17 }, new int[] { 72, 75, 3 }, new int[] { 73, 90, 34 }, new int[] { 39, 46, 75 }, new int[] { 2, 6, 18 }, new int[] { 77, 93, 7 }, new int[] { 36, 46, 73 }, new int[] { 18, 85, 12 }, new int[] { 23, 43, 71 }, new int[] { 8, 14, 7 }, new int[] { 78, 91, 55 }, new int[] { 80, 84, 88 }, new int[] { 9, 77, 64 }, new int[] { 51, 56, 96 }, new int[] { 4, 6, 85 }, new int[] { 96, 96, 13 }, new int[] { 9, 82, 26 }, new int[] { 75, 78, 58 }, new int[] { 7, 41, 53 }, new int[] { 12, 86, 21 }, new int[] { 82, 83, 63 }, new int[] { 5, 48, 81 }, new int[] { 19, 91, 14 }, new int[] { 2, 92, 71 }, new int[] { 83, 93, 66 }, new int[] { 6, 11, 80 }, new int[] { 42, 94, 65 }, new int[] { 38, 44, 8 }, new int[] { 21, 29, 61 }, new int[] { 50, 61, 2 } };
            //new int[][] { new int[] { 1, 3, 4 }, new int[] { 2, 4, 1 }, new int[] { 1, 1, 4 }, new int[] { 3, 5, 1 }, new int[] { 2, 5, 5 } };
            int k = 40;
            // 3;
            Console.WriteLine(new P1751最多可以参加的会议数目II().MaxValue(input, k));
        }
    }
}
