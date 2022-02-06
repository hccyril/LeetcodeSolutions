using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0300最长递增子序列
    {
        public int LengthOfLIS(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            int max = 1;
            int[] dp = new int[nums.Length];
            for (int i = 0; i < nums.Length; ++i)
            {
                    dp[i] = 1;
                for (int j = i - 1; j >= 0; --j)
                {
                    if (nums[j] < nums[i])
                    {
                        int n = dp[j] + 1;
                        if (n > dp[i])
                        {
                            dp[i] = n;
                            if (n > max) max = n;
                        }
                    }
                }
            }
            return max;
        }
    }
}
