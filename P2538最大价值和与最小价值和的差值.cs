using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 难题挑战 2023/12/24 rating 2397
// 用时 1:04:57 + 1WA >_<||
// 典型的后序遍历，要处理的形况比较复杂
internal class P2538最大价值和与最小价值和的差值
{
    public long MaxOutput(int n, int[][] edges, int[] price)
    {
        if (n == 1) return 0;
        var g = edges.TreeGraph(n);
        long maxcost = 0L;
        (long hc, long nc, int id) Dfs(int i, int p)
        {
            (long hc, long nc, int id) h1 = (-1, -1, -1), h2 = (-1, -1, -1), n1 = (-1, -1, -1), n2 = (-1, -1, -1);
            foreach (int j in g[i]) if (j != p)
                {
                    var t = Dfs(j, i);
                    if (t.hc > h1.hc) (h1, h2) = (t, h1);
                    else if (t.hc > h2.hc) h2 = t;
                    if (t.nc > n1.nc) (n1, n2) = (t, n1);
                    else if (t.nc > n2.nc) n2 = t;
                }
            if (h1.id < 0) 
            {
                return (price[i], 0, i);
            }
            else if (h2.id >= 0)
            {
                if (h1.id == n1.id)
                {
                    maxcost = Math.Max(maxcost, price[i] + Math.Max(h1.hc + n2.nc, n1.nc + h2.hc));
                }
                else
                {
                    maxcost = Math.Max(maxcost, price[i] + h1.hc + n1.nc);
                }
            }

            if (p < 0) // root - calc cost
            {
                maxcost = Math.Max(maxcost, Math.Max(h1.hc, price[i] + n1.nc));
            }

            return (h1.hc + price[i], n1.nc + price[i], i);
        }

        Dfs(0, -1);

        return maxcost;
    }
}
