using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// medium, 2024/6/4 Daily
// 1908
// LC第一次记录带权的无向树
internal class P3067在带权树网络中统计可连接服务器对数目
{
    public int[] CountPairsOfConnectableServers(int[][] edges, int signalSpeed)
    {
        int n = edges.Length + 1;
        var tg = edges.WeightedTreeGraph();
        var a = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();

        int Dfs(int i = 0, int p = -1, long s = 0L)
        {
            // Console.WriteLine(" {0} {1} {2}", i, p, s);
            int cnt = 0;
            if (s > 0 && s % signalSpeed == 0) ++cnt;
            foreach ((int j, int w) in tg[i])
                if (j != p) // 隔了一段时间没写treedfs结果连这句都漏掉了，还排查了半天！！
                {
                    int c = Dfs(j, i, s + w);
                    if (p < 0 && c > 0)
                        a[i].Add(c);
                    cnt += c;
                }
            return cnt;
        }

        for (int i = 0; i < n; ++i)
            Dfs(i);

        var ans = new int[n];
        for (int k = 0; k < n; ++k)
        {
            var li = a[k];
            if (li.Count > 1)
            {
                for (int i = 0; i < li.Count - 1; ++i)
                    for (int j = i + 1; j < li.Count; ++j)
                        ans[k] += li[i] * li[j];
            }
        }
        return ans;
    }
}
