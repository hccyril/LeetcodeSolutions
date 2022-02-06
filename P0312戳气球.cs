using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0312戳气球
    {
        int Prod(int[] nums, int s, int i, int e)
            => (s >= 0 ? nums[s] : 1) * nums[i] * (e < nums.Length ? nums[e] : 1);
        public int MaxCoins(int[] nums)
        {
            int[,] dp = new int[500, 500];
            for (int len = 0; len < nums.Length; ++len)
                for (int start = 0; start + len < nums.Length; ++start)
                {
                    int end = start + len;
                    for (int i = start; i <= end; ++i)
                    {
                        int sum = 0;
                        if (i > start) sum += dp[start, i - 1];
                        if (i < end) sum += dp[i + 1, end];
                        sum += Prod(nums, start - 1, i, end + 1);
                        if (sum > dp[start, end]) dp[start, end] = sum;
                    }
                }
            return dp[0, nums.Length - 1];
        }
    }
}
