using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛题
    // 理解之后就是贪心
    internal class P2136全部开花的最早一天
    {
        public int EarliestFullBloom(int[] plantTime, int[] growTime)
        {
            int pdays = 0, max = 0;
            foreach ((int pt, int gt) in plantTime.Zip(growTime, (pt, gt) => (pt, gt)).OrderByDescending(t => t.gt))
                max = Math.Max(max, (pdays += pt) + gt);
            return max;
        }
    }
}
