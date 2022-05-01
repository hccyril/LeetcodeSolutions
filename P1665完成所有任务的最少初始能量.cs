using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/12
    // rank: 1901
    // rank低是因为容易猜出来是贪心法，真正证明见题解
    internal class P1665完成所有任务的最少初始能量
    {
        public int MinimumEffort(int[][] tasks)
            => tasks.OrderBy(t => t[1] - t[0]).Aggregate(0, (e, t) => Math.Max(e + t[0], t[1]));
    }
}
