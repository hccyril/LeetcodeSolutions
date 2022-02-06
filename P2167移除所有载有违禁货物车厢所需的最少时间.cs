using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/6 周赛279 D
    // 开始：30:00 结束 56:54 用时26分钟
    internal class P2167移除所有载有违禁货物车厢所需的最少时间
    {
        public int MinimumTime(string s)
        {
            int[] dp = new int[s.Length];
            for (int i = 0; i < s.Length; ++i)
            {
                if (i > 0) dp[i] = dp[i - 1];
                if (s[i] == '1') dp[i] = Math.Min(dp[i] + 2, i + 1);
            }
            int cost = dp[s.Length - 1];
            int[] dpR = new int[s.Length];
            for (int i = s.Length - 1; i >= 0; --i)
            {
                if (i < s.Length - 1) dpR[i] = dpR[i + 1];
                if (s[i] == '1') dpR[i] = Math.Min(dpR[i] + 2, s.Length - i);
                cost = Math.Min(cost, (i > 0 ? dp[i - 1] : 0) + dpR[i]);
            }
            return cost;
        }
    }
}
