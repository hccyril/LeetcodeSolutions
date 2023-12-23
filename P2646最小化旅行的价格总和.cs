using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2023/12/6 Daily
// Graph
// 先dfs找出路径，并算出每个节点的总成本，然后应用打家劫舍算法求最优解
internal class P2646最小化旅行的价格总和
{
    public int MinimumTotalPrice(int n, int[][] edges, int[] price, int[][] trips)
    {
        var ug = edges.UndirectedGraphNoLength();
        List<int> Dfs(int target, int i, int p, List<int> pl)
        {
            pl.Add(i);
            if (i == target) return pl;
            foreach (int j in ug[i]) if (j != p)
                {
                    var a = Dfs(target, j, i, pl);
                    if (a.Any() && a[^1] == target) return a;
                }
            pl.RemoveAt(pl.Count - 1);
            return pl;
        }
        int[] costs = new int[n];
        foreach (var tp in trips)
        {
            var pa = Dfs(tp[1], tp[0], -1, new());
            foreach (int v in pa)
                costs[v] += price[v];
        }
        (int, int) PostOrder(int i, int p)
        {
            int d1 = costs[i] >> 1, d0 = costs[i];
            foreach (int j in ug[i]) if (j != p)
                {
                    (int s1, int s0) = PostOrder(j, i);
                    d1 += s0;
                    d0 += s1;
                }
            return (Math.Min(d1, d0), d0);
        }
        return PostOrder(0, -1).Item1;
    }
}
