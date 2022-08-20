using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/8/18
    // rank: 2381 经典题
    // DP，用什么作为Key，如何构建是关键
    internal class P0956最高的广告牌
    {
        public int TallestBillboard(int[] rods)
        {
            Dictionary<int, int> dp = new();
            int Update(int k, int v) => dp.TryAdd(k, v) ? 0 : v > dp[k] ? dp[k] = v : -1;
            Update(0, 0);
            foreach (int r in rods)
            {
                foreach ((int k, int v) in dp.ToArray())
                {
                    Update(k + r, v + r);
                    Update(Math.Abs(k - r), Math.Max(v, v - k + r));
                }
                Update(r, r);
            }
            return dp[0];
        }
    }
}
