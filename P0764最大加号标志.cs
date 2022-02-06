using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // DP
    class P0764最大加号标志
    {
        // 没做，官方题解
        public int orderOfLargestPlusSign(int n, int[][] mines)
        {
            var banned = new HashSet<int>();
            int[,] dp = new int[n,n];

            foreach (int[] mine in mines)
                banned.Add(mine[0] * n + mine[1]);
            int ans = 0, count;

            for (int r = 0; r < n; ++r)
            {
                count = 0;
                for (int c = 0; c < n; ++c)
                {
                    count = banned.Contains(r * n + c) ? 0 : count + 1;
                    dp[r,c] = count;
                }

                count = 0;
                for (int c = n - 1; c >= 0; --c)
                {
                    count = banned.Contains(r * n + c) ? 0 : count + 1;
                    dp[r,c] = Math.Min(dp[r,c], count);
                }
            }

            for (int c = 0; c < n; ++c)
            {
                count = 0;
                for (int r = 0; r < n; ++r)
                {
                    count = banned.Contains(r * n + c) ? 0 : count + 1;
                    dp[r,c] = Math.Min(dp[r,c], count);
                }

                count = 0;
                for (int r = n - 1; r >= 0; --r)
                {
                    count = banned.Contains(r * n + c) ? 0 : count + 1;
                    dp[r,c] = Math.Min(dp[r,c], count);
                    ans = Math.Max(ans, dp[r,c]);
                }
            }

            return ans;
        }
    }
}
