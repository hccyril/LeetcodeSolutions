using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // extend from P0123买卖股票的最佳时机III
    class P0188买卖股票的最佳时机IV
    {
        public int MaxProfit(int k, int[] prices)
        {
            if (!prices.Any() || k == 0) return 0;
            int[,] dp = new int[prices.Length, k];
            for (int t = 0; t < k; ++t)
                for (int day = 0; day < prices.Length; ++day)
                {
                    if (day > 0) dp[day, t] = dp[day - 1, t];
                    for (int bd = 0; bd < day; ++bd)
                    {
                        int earn = (bd > 0 && t > 0 ? dp[bd - 1, t - 1] : 0) + prices[day] - prices[bd];
                        if (earn > dp[day, t]) dp[day, t] = earn;
                    }
                }
            return dp[prices.Length - 1, k - 1];
        }

        internal static void Run()
        {
            Console.WriteLine(new P0188买卖股票的最佳时机IV().MaxProfit(2, new int[0]));
        }
    }
}
