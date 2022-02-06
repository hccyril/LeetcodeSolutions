using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{

    class P0718最长重复子数组
    {
        /**
         * 比较典型的动态规划
         * 留意还可以用滑动窗口
         * 没时间做，以下为官方题解
         * */
        public int FindLength(int[] nums1, int[] nums2)
        {
            int n = nums1.Length, m = nums2.Length;
            int[,] dp = new int[n + 1,m + 1];
            int ans = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = m - 1; j >= 0; j--)
                {
                    dp[i,j] = nums1[i] == nums2[j] ? dp[i + 1,j + 1] + 1 : 0;
                    ans = Math.Max(ans, dp[i,j]);
                }
            }
            return ans;
        }
    }
}
