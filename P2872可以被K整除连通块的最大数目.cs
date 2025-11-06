using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/12/21 US Daily
// rating 1967
// tree dfs
internal class P2872可以被K整除连通块的最大数目
{
    public int MaxKDivisibleComponents(int n, int[][] edges, int[] values, int k)
    {
        var tg = edges.TreeGraph();
        long[] st = new long[n];
        int ans = 0;

        // Tree DFS
        const int DFS_ROOT = 0;
        Stack<(int Node, int Child)> dfsStk = new();
        int i = DFS_ROOT, childIndex = 0;
        while (true)
        {
            if (childIndex == tg[i].Count)
            {
                int parent = dfsStk.Any() ? dfsStk.Peek().Node : -1;
                st[i] = tg[i].Where(j => j != parent).Select(j => st[j]).Sum() + values[i];
                if (st[i] % k == 0) ++ans;

                if (dfsStk.Any())
                {
                    (i, childIndex) = dfsStk.Pop();
                    continue;
                }
                else break;
            }

            int nextIndex = tg[i][childIndex];

            if (dfsStk.Any() && dfsStk.Peek().Node == nextIndex)
            {
                ++childIndex;
                continue;
            }
            else
            {   
                dfsStk.Push((i, childIndex + 1));
                (i, childIndex) = (nextIndex, 0);
            }
        }

        return ans;
    }

    public static readonly string RefMethod = nameof(GraphEX.TreeDfs2);
}
