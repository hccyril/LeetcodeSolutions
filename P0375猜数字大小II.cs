using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium
    // DP
    class P0375猜数字大小II
    {
        int[,] dp;
        int Guess(int start, int end)
        {
            if (start > end) return 0;
            else if (dp[start, end] >= 0) return dp[start, end];
            else if (start == end) return dp[start, end] = 0;
            int min = -1;
            for (int mid = start; mid <= end; ++mid)
            {
                int pay = mid + Math.Max(Guess(start, mid - 1), Guess(mid + 1, end));
                if (min == -1 || pay < min) min = pay;
            }
            return dp[start, end] = min;
        }
        public int GetMoneyAmount(int n)
        {
            dp = new int[n + 1, n + 1];
            for (int i = 0; i <= n; ++i)
                for (int j = 0; j <= n; ++j)
                    dp[i, j] = -1;
            return Guess(1, n);
        }
    }
}
