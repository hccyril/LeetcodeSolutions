using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/16
    // rank: 2085
    // Tarjan
    internal class P1192查找集群内的关键连接
    {
        void Tarjan(int n)
        {
            dfn = new int[n];
            low = new int[n];
            Dfs(0);
#if DEBUG
            for (int i = 0; i < n; ++i) Console.Write(i + " ");
            Console.WriteLine();
            Console.WriteLine(string.Join(" ", dfn));
            Console.WriteLine(string.Join(" ", low));
#endif
        }
        int[] dfn, low;
        int timestamp = 0;
        int Dfs(int i, int parent = -1)
        {
            low[i] = dfn[i] = ++timestamp;
            foreach (int j in ug[i])
            {
                if (dfn[j] == 0)
                {
                    int low_ret = Dfs(j, i);
                    // tarjan定义：桥的判定条件：dfn[u] < low[v]
                    if (low_ret > dfn[i]) ansList.Add(new List<int>() { i, j });

                    // 回溯时更新low的值
                    low[i] = Math.Min(low[i], low_ret);
                }
                else if (j != parent) // 注意不能沿原路返回
                {
                    // 目标节点已经访问过，更新low值
                    low[i] = Math.Min(low[i], dfn[j]);
                }
            }

            return low[i];
        }

        Dictionary<int, List<int>> ug;
        IList<IList<int>> ansList;

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            ug = new();
            foreach (var ed in connections)
            {
                int a = ed[0], b = ed[1];
                ug.TryAdd(a, new());
                ug[a].Add(b);
                ug.TryAdd(b, new());
                ug[b].Add(a);
            }

            ansList = new List<IList<int>>();
            Tarjan(n);
            return ansList;
        }
    }
}
