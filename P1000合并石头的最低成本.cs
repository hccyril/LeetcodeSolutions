using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/9
    // 动态规划+回溯
    // （未尝试）改成记忆化回溯应该会简单点
    internal class P1000合并石头的最低成本
    {
        public int MergeStones(int[] stones, int k)
        {
            if (stones.Length == 1) return 0; // 坑！一个WA
            if (stones.Length < k || (stones.Length - 1) % (k - 1) != 0) return -1;
            // 前缀和
            PreSum preSum = new(stones);
            // dp
            int[,] dp = new int[stones.Length, stones.Length];
            for (int size = k; size <= stones.Length; size += k - 1)
            {
                for (int i = 0, j = size - 1; j < stones.Length; ++i, ++j)
                {
                    //Console.WriteLine("[{0}, {1}] -----", i, j);

                    int rsum = preSum.RangeSum(i, j);
                    if (size <= k) dp[i, j] = rsum;
                    else
                    {
                        // 用栈模拟回溯
                        Span<int> stk = stackalloc int[k];
                        int idx = 0, end = i, sum = 0;
                        while (idx >= 0)
                        {
                            //Console.WriteLine("idx={0} end={1} sum={2}", idx, end, sum);
                            if (idx == k)
                            {
                                if (end == j + 1)
                                    if (dp[i, j] == 0 || sum + rsum < dp[i, j])
                                        dp[i, j] = sum + rsum;
                                --idx;
                            }
                            else if (stk[idx] > 0)
                            {
                                int count = stk[idx]; stk[idx] = 0; end -= count;
                                sum -= dp[end, end + count - 1];
                                count += k - 1;
                                if (end + count <= j + 1)
                                {
                                    sum += dp[end, end + count - 1];
                                    stk[idx++] = count;
                                    end += count;
                                }
                                else --idx;
                            }
                            else if (stk[idx] == 0)
                            {
                                if (end <= j)
                                {
                                    stk[idx++] = 1;
                                    ++end;
                                }
                                else --idx;
                            }
                        }
                        //dp[i, j] = int.MaxValue;
                        //for (int s = i, e = i + size - k; e <= j; ++s, ++e)
                        //{
                        //    dp[i, j] = Math.Min(dp[i, j], dp[s, e] + rsum);
                        //}
                    }
                }
            }
            return dp[0, stones.Length - 1];
        }

        internal static void Run()
        {
            int[] stones = { 3, 2, 4, 1 };
            int k = 2;
            int ans = new P1000合并石头的最低成本().MergeStones(stones, k);
            Console.WriteLine("P1000 ans = " + ans);
        }
    }
}
