using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 2023/12/23 难题45分挑战
// hard->medium, rating 2405
// 完成，time left 10:34
// 完全就是中等题的难度
internal class P2747统计没有收到请求的服务器数目
{
    public int[] CountServers(int n, int[][] logs, int x, int[] queries)
    {
        var sa = logs.Select(t => (t[1], t[0])).OrderBy(t => t).ToArray();
        int l = 0, r = -1;
        Dictionary<int, int> cnt = new();
        int[] ans = new int[queries.Length];
        foreach ((int t, int i) in Enumerable.Range(0, queries.Length).Select(i => (queries[i], i)).OrderBy(t => t))
        {
            while (r < sa.Length - 1 && sa[r + 1].Item1 <= t)
            {
                (_, int s) = sa[++r];
                if (!cnt.TryAdd(s, 1))
                    ++cnt[s];
            }
            while (l <= r && sa[l].Item1 < t - x)
            {
                (_, int s) = sa[l++];
                if (--cnt[s] == 0)
                    cnt.Remove(s);
            }
            ans[i] = n - cnt.Count;
        }
        return ans;
    }
}
