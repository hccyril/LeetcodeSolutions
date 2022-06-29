using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/28
    // DP O(n^2)
    internal class Pi面试题0813堆箱子
    {
        public int PileBox(int[][] box)
        {
            var ba = box.Select(t => ((int w, int d, int h))(t[0], t[1], t[2]))
                .OrderByDescending(t => t.h)
                .ToArray();
            Span<int> dp = stackalloc int[ba.Length];
            int ans = 0;
            for (int i = 0; i < ba.Length; ++i)
            {
                (int w, int d, int h) = ba[i];
                dp[i] = Math.Max(h, dp[i]);
                if (dp[i] > ans) ans = dp[i];
                for (int j = i + 1; j < ba.Length; ++j)
                    if (ba[j].d < d && ba[j].w < w && ba[j].h < h)
                        dp[j] = Math.Max(dp[j], dp[i] + ba[j].h);
            }
            return ans;
        }
    }
}
