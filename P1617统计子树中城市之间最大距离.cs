using ConsoleCore1;
using System;
using System.Collections.Generic;
using System.Linq;

// hard, practice 2023/7/10
// 超时：用时1小时20分37秒
// 万万没想到用暴力枚举
internal class P1617统计子树中城市之间最大距离
{
    // bfs
    bool IsTree(int[] ug, int m)
    {
        int n = ug.Length;
        Queue<int> qu = new();
        int b = 0;
        for (int i = 0; i < n; ++i)
            if ((m & 1 << i) != 0)
            {
                b |= 1 << i;
                qu.Enqueue(i);
                break;
            }
        while (qu.Any())
        {
            int i = qu.Dequeue();
            for (int j = 0; j < n; ++j)
                if ((ug[i] & 1 << j) != 0 && (m & 1 << j) != 0 && (b & 1 << j) == 0)
                {
                    b |= 1 << j;
                    if (b == m) return true;
                    qu.Enqueue(j);
                }
        }
        return false;
    }

    // dfs
    int GetLen(int[] ug, int m)
    {
        int n = ug.Length;
        int s = Enumerable.Range(0, n).First(i => (m & 1 << i) != 0); 
        (int, int) Dfs(int i, int p)
        {
            int h1 = -1, h2 = -1, mh = 0, md = 0;
            for (int j = 0; j < n; ++j)
                if ((m & 1 << j) != 0 && (ug[i] & 1 << j) != 0 && j != p)
                {
                    (int h, int d) = Dfs(j, i);
                    mh = Math.Max(mh, h + 1);
                    md = Math.Max(md, d);
                    if (h + 1 > h1)
                    {
                        h2 = h1;
                        h1 = h + 1;
                    }
                    else if (h + 1 > h2)
                    {
                        h2 = h + 1;
                    }
                }
            md = Math.Max(md, h1 > 0 && h2 > 0 ? h1 + h2 : 0);
            return (mh, Math.Max(mh, md));
        }
        return Dfs(s, -1).Item2;
    }

    public int[] CountSubgraphsForEachDiameter(int n, int[][] edges)
    {
        int[] ug = new int[n];
        foreach (var ed in edges)
        {
            int u = ed[0] - 1, v = ed[1] - 1;
            ug[u] |= 1 << v;
            ug[v] |= 1 << u;
        }

        int[] a = new int[n - 1];
        int fn = 1 << n;
        for (int m = 3; m < fn; ++m)
            if ((m & m - 1) != 0 && IsTree(ug, m))
            {
                int len = GetLen(ug, m);
                // DEBUG
                Console.WriteLine("GetLen( {0} )={1} (m={2})",
                    string.Join(" ", Enumerable.Range(0, n).Where(i => (m & 1 << i) != 0).Select(i => i + 1)),
                    len, m);
                a[len - 1]++;
            }
        return a;
    }

    internal static void Run()
    {
        int n = 4;
        var edges = "[[1,2],[2,3],[2,4]]".ToTestInput<int[][]>();
        var sln = new P1617统计子树中城市之间最大距离();
        var a = sln.CountSubgraphsForEachDiameter(n, edges);
        Console.WriteLine(string.Join(" ", a));
    }
}

