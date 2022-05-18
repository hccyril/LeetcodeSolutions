using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/1
    // DP
    internal class P1458两个子序列的最大点积
    {
        public int MaxDotProduct(int[] nums1, int[] nums2)
        {
            // 快速筛选一个数组全是正数而另一个数组全是负数的特殊情况
            bool b1p = true, b1m = true, b2p = true, b2m = true;
            foreach (var n1 in nums1)
                if (!b1p && !b1m) break;
                else if (n1 == 0) b1p = b1m = false;
                else if (n1 < 0) b1p = false;
                else if (n1 > 0) b1m = false;
            foreach (var n2 in nums2)
                if (!b2p && !b2m) break;
                else if (n2 == 0) b2p = b2m = false;
                else if (n2 < 0) b2p = false;
                else if (n2 > 0) b2m = false;
            if (b1p && b2m || b1m && b2p)
            {
                int max = int.MinValue;
                for (int i = 0; i < nums1.Length; i++)
                    for (int j = 0; j < nums2.Length; j++)
                        max = Math.Max(max, nums1[i] * nums2[j]);
                return max;
            }

            // start DP
            int[,] dp = new int[nums1.Length, nums2.Length];
            for (int i = 0; i < nums1.Length; i++)
            {
                for (int j = 0; j < nums2.Length; j++)
                {
                    dp[i, j] = nums1[i] * nums2[j];
                    if (dp[i, j] < 0) dp[i, j] = 0;
                    if (i > 0 && j > 0) dp[i, j] += dp[i - 1, j - 1];
                    if (i > 0) dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j]);
                    if (j > 0) dp[i, j] = Math.Max(dp[i, j], dp[i, j - 1]);
                }
            }

            return dp[nums1.Length - 1, nums2.Length - 1];
        }
    }
}
