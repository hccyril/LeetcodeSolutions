using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2023/7/11 Practice, rank: 2310
    // 通过：完成时间27分50秒
    // DP O(k*n^2)
    internal class P1959K次调整数组大小浪费的最小总空间
    {
        public int MinSpaceWastedKResizing(int[] nums, int k)
        {
            int n = nums.Length, h = 0;
            int[] dp = new int[n];
            for (int i = 0; i < n; ++i)
            {
                h = Math.Max(h, nums[i]);
                for (int j = 0; j <= i; ++j)
                    dp[i] += h - nums[j];
            }
            while (k-- > 0)
            {
                for (int i = n - 1; i > 0; --i)
                {
                    h = nums[i];
                    // 枚举最后一次调整点
                    for (int j = i; j > 0; --j)
                    {
                        int d = dp[j - 1];
                        h = Math.Max(h, nums[j]);
                        for (int p = j; p <= i; ++p)
                            d += h - nums[p];
                        dp[i] = Math.Min(dp[i], d);
                    }
                }
            }
            return dp[^1];

        }
    }
}
