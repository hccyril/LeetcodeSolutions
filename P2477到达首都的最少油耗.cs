using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // mediium, 2023/2/12 US Daily
    // 只是一题中等题，但想了半天才发现很简单，比赛时肯定GG了
    internal class P2477到达首都的最少油耗
    {
        public long MinimumFuelCost(int[][] roads, int seats)
        {
            if (!roads.Any()) return 0; // 又忘记边界条件了！！

            var ug = roads.UndirectedGraphNoLength();

            (long, int) DpDfs(int i, int pr)
            {
                long fuel = 0;
                int people = 1;

                foreach (int j in ug[i])
                    if (j != pr)
                    {
                        (long f, int p) = DpDfs(j, i);
                        fuel += f + (p + seats - 1) / seats;
                        people += p;
                    }

                return (fuel, people);
            }

            return DpDfs(0, -1).Item1;
        }
    }
}
