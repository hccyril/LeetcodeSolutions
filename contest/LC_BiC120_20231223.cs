namespace ConsoleCore1.contest;

//# 第120场双周赛，2023/12/23
//# AK but WA 1 time for D
//# 190 / 2534	呱呱编程实验室 	18	1:20:56	0:03:56	0:14:00	1:15:56	0:59:20  1
internal class LC_BiC120_20231223
{
    #region Problem A
    public int SolveA(int x)
    {
        return x;
    }
    #endregion

    #region Problem B
    public int SolveB(int x)
    {
        return x;
    }
    #endregion

    #region Problem C
    public int SolveC(int x)
    {
        return x;
    }
    #endregion

    // 一开始想当然以为负数只需要维护两个，结果WA
    // 幸亏WA用例是公开的，不然估计找不到问题原因
    #region Problem D
    class CoinStruct
    {
        internal int p1 = 0, p2 = 0, p3 = 0, m1 = 0, m2 = 0, m3 = 0;
        public void Add(int x)
        {
            if (x > 0)
            {
                if (x > p1) (p1, p2, p3) = (x, p1, p2);
                else if (x > p2) (p2, p3) = (x, p2);
                else if (x > p3) p3 = x;
            }
            else
            {
                if (x < m1) (m1, m2, m3) = (x, m1, m2);
                else if (x < m2) (m2, m3) = (x, m2);
                else if (x < m3) m3 = x;
            }
        }

        public void Merge(CoinStruct cs)
        {
            int[] a = { cs.p1, cs.p2, cs.p3, cs.m1, cs.m2, cs.m3 };
            foreach (int x in a.Where(t => t != 0))
                Add(x);
        }

        public long GetP()
        {
            int[] a = { p1, p2, p3, m1, m2, m3 };
            a = a.Where(t => t != 0).ToArray();
            long ans = 1L;
            if (a.Length < 3) return 1L;
            else if (a.Length == 3)
            {
                foreach (int x in a) ans *= x;
                return ans < 0L ? 0 : ans;
            }
            // ppp
            if (p3 > 0) ans = ans * p1 * p2 * p3;
            // pmm
            if (m2 < 0) ans = Math.Max(ans, (long)p1 * m1 * m2);
            return ans;
        }
    }
    public long[] PlacedCoins(int[][] edges, int[] cost)
    {
        int n = edges.Length + 1;
        List<int>[] g = new List<int>[n];
        for (int i = 0; i < n; ++i) g[i] = new();
        foreach (var ed in edges)
        {
            g[ed[0]].Add(ed[1]);
            g[ed[1]].Add(ed[0]);
        }

        long[] ans = new long[n];

        CoinStruct Dfs(int i, int p)
        {
            CoinStruct cs = new();
            cs.Add(cost[i]);
            foreach (int j in g[i])
                if (j != p)
                {
                    var cc = Dfs(j, i);
                    cs.Merge(cc);
                }
            ans[i] = cs.GetP();
            return cs;
        }

        Dfs(0, -1);
        return ans;
    }
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_BiC120_20231223 sln = new();

        return p switch
        {
            'A' => sln.RunTestA(),
            'B' => sln.RunTestB(),
            'C' => sln.RunTestC(),
            'D' => sln.RunTestD(),
            'E' => sln.RunTestE(),
            _ => -1
        };
    }

    int RunTestA()
    {
        return 0;
    }

    int RunTestB()
    {
        return 0;
    }

    int RunTestC()
    {
        return 0;
    }

    int RunTestD()
    {
        var edges = Common.ToTestInput<int[][]>("[[0,8],[8,1],[9,2],[4,6],[7,4],[3,7],[3,8],[5,8],[5,9]]");
        var costs = Common.ToTestInput<int[]>("[-4,83,-97,40,86,-85,-6,-84,-16,-53]");
        var ans = PlacedCoins(edges, costs);
        Console.WriteLine(string.Join(" ", ans));
        return 0;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
