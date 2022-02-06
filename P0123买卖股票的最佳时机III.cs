using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // extend: P0188买卖股票的最佳时机IV
    class P0123买卖股票的最佳时机III
    {
        public int MaxProfit(int[] prices)
        {
            int[] larr = new int[prices.Length], rarr = new int[prices.Length];
            int buy = -1, sell = -1, earn = 0;
            for (int i = 0; i < prices.Length; ++i)
            {
                if (buy == -1 || buy > prices[i]) buy = prices[i];
                if (prices[i] - buy > earn) earn = prices[i] - buy;
                larr[i] = earn;
            }
            earn = 0;
            for (int i = prices.Length - 1; i >= 0; --i)
            {
                if (sell == -1 || sell < prices[i]) sell = prices[i];
                if (sell - prices[i] > earn) earn = sell - prices[i];
                rarr[i] = earn;
            }
            earn = 0;
            for (int i = 0; i <= prices.Length; ++i)
            {
                int sum = (i > 0 ? larr[i - 1] : 0) + (i < prices.Length ? rarr[i] : 0);
                if (sum > earn) earn = sum;
            }
            return earn;
        }
    }
}
