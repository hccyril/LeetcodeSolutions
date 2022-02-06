using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, DP, 2021/12/08 daily
    internal class P0689三个无重叠子数组的最大和
    {
        public int[] MaxSumOfThreeSubarrays(int[] nums, int k)
        {
            int[] sums = new int[nums.Length - k + 1];
            sums[0] = Enumerable.Range(0, k).Sum(i => nums[i]);
            for (int start = 1, end = k; end < nums.Length; 
                sums[start] = sums[start - 1] - nums[start - 1] + nums[end++], ++start) ;
            // 公式：dp[i, n] 表示 0..n 区间里取 n 个长度为 k 的子数组的最大总和
            int[,] dp = new int[nums.Length, 3], index = new int[nums.Length, 3];

            // dp
            for (int j = 0; j < 3; ++j)
            {
                int msi = j * k;
                int ms = sums[msi] + (j == 0 ? 0 : dp[msi - 1, j - 1]);
                int limit = nums.Length - (2 - j) * k;
                for (int i = (j + 1) * k - 1; i < limit; ++i)
                {
                    int si = i - k + 1, s = sums[si] + (j == 0 ? 0 : dp[si - 1, j - 1]);
                    if (s > ms)
                    {
                        ms = s;
                        msi = si;
                    }
                    dp[i, j] = ms;
                    index[i, j] = msi;
                }
            }

            // return
            int[] ans = new int[3];
            ans[2] = index[nums.Length - 1, 2];
            ans[1] = index[ans[2] - 1, 1];
            ans[0] = index[ans[1] - 1, 0];
            return ans;
        }

        internal static void Run()
        {
            var so = new P0689三个无重叠子数组的最大和();
            int[] input = { 1, 2, 1, 2, 6, 7, 5, 1 };
            int k = 2;
            var ans = so.MaxSumOfThreeSubarrays(input, k);
            Console.WriteLine(string.Join(", ", ans));
        }
    }
}
