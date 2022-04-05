using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P2039网络空闲的时刻
    {
        public int NetworkBecomesIdle(int[][] edges, int[] patience)
        {
            int Time(int len, int pt)
            {
                int trip = 2 * len;
                int resend = (trip - 1) / pt * pt;
                return trip + resend + 1;
            }
            var paths = edges.BfsPaths(patience.Length);
            return Enumerable.Range(1, patience.Length - 1).Select(i => Time(paths[i], patience[i])).Max();
        }
    }
}
