using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, DP
// 初次完成：2021/9/4 15:56
// 2023再次提交时发现一个用例超时
// 2024/4/29 重写
class P0834树中距离之和
{
    // 2024/4/29 version2 
    // 换根DP
    public int[] SumOfDistancesInTree(int n, int[][] edges)
    {
        var tg = edges.TreeGraph();

        (int, int)[] dp = new (int, int)[n];
        (int, int) Dfs1(int i, int p)
        {
            //Console.WriteLine("Dfs1 {0} {1}", i, p);
            int c = 1, s = 0;
            foreach (int j in tg[i])
                if (j != p)
                {
                    (int d, int t) = Dfs1(j, i);
                    c += d;
                    s += t + d;
                }
            return dp[i] = (c, s);
        }
        Dfs1(0, -1);

        int[] ans = new int[n];
        void Dfs2(int i, int p)
        {
            //Console.WriteLine("Dfs2 {0} {1}", i, p);

            foreach (int j in tg[i])
                if (j != p)
                {
                    int c_j = dp[j].Item1, c_rest = n - c_j,
                        s_j = dp[j].Item2,
                        s_rest = ans[i] - (s_j + c_j);
                    ans[j] = s_j + s_rest + c_rest;
                    Dfs2(j, i);
                }
        }
        ans[0] = dp[0].Item2;
        Dfs2(0, -1);

        return ans;
    }

    List<int>[] leaves;
    Dictionary<int, int[]> dic = new Dictionary<int, int[]>();

    // 当r为父节点时子节点的路径和及节点个数
    int[] SumNode(int r, int c)
    {
        int key = (r << 16) | c;
        if (dic.ContainsKey(key)) return dic[key];
        int sum = 0, count = 1;
        foreach (var cc in leaves[c])
            if (cc != r)
            {
                var ret = SumNode(c, cc);
                sum += ret[0] + ret[1];
                count += ret[1];
            }
        return dic[key] = new int[] { sum, count };
    }
    int SumTree(int r)
    {
        int sum = 0;
        foreach (var c in leaves[r])
        {
            int[] ret = SumNode(r, c);
            sum += ret[0] + ret[1];
        }
        return sum;
    }
    public int[] SumOfDistancesInTree_ver1(int n, int[][] edges)
    {
        leaves = new List<int>[n];
        for (int i = 0; i < n; ++i) leaves[i] = new List<int>();
        foreach (var e in edges)
        {
            int a = e[0], b = e[1];
            leaves[a].Add(b);
            leaves[b].Add(a);
        }
        int[] ans = new int[n];
        for (int i = 0; i < n; ++i)
            ans[i] = SumTree(i);
        return ans;
    }
}
