using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCore1;

namespace ConsoleCore1.contest
{
    internal class LCP_202305
    {
        #region Problem A
        public int SolveA(int x)
        {
            return x;
        }
        #endregion

        #region Problem B
        public int RampartDefensiveLine(int[][] rampart)
        {
            int n = rampart.Length;
            int[] ain = new int[n - 1];
            for (int i = 1; i < n; ++i)
                ain[i - 1] = rampart[i][0] - rampart[i - 1][1];
            int l = 0, r = rampart.Last()[1] - rampart.First()[0] + 1;
            bool Check(int k)
            {
                int space = ain[0];
                for (int i = 1; i < n - 1; ++i)
                {
                    space += ain[i];
                    if (space < k) return false;
                    space = Math.Min(space - k, ain[i]);
                }
                return true;
            }
            while (l < r)
            {
                int m = l + r + 1 >> 1;
                if (Check(m)) l = m;
                else r = m - 1;
            }
            return l;
        }
        #endregion

        #region Problem C

        public int ExtractMantra(string[] matrix, string mantra)
        {
            int m = matrix.Length, n = matrix[0].Length;
            (int, int)[,] visit = new (int, int)[m, n];
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    visit[i, j] = (-1, 0);
            Queue<(int, int, int, int)> qu = new();
            qu.Enqueue((0, 0, 0, 0));
            while (qu.Any())
            {
                (int i, int j, int li, int si) = qu.Dequeue();
                (int l, int s) = visit[i, j];
                if (li <= l) continue;
                visit[i, j] = (li, si);
                if (matrix[i][j] == mantra[li])
                {
                    ++si;
                    if (++li == mantra.Length) return si;
                    //visit[i, j] = (len, step);
                    qu.Enqueue((i, j, li, si));
                }
                else foreach ((int ni, int nj) in GraphEX.FourDir(m, n, i, j))
                    {
                        (int l0, int s0) = visit[ni, nj];
                        if (li > l0) qu.Enqueue((ni, nj, li, si + 1));
                    }
            }
            return -1;
        }

        internal static int Dist((int x, int y) p1, (int x, int y) p2)
            => Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);

        public int ExtractMantra_2(string[] matrix, string mantra)
        {
            List<(int, int)>[] a = new List<(int, int)>[26];
            for (int i = 0; i < 26; ++i) a[i] = new();
            int m = matrix.Length, n = matrix[0].Length;
            int[,] dp = new int[m, n];
            dp[0, 0] = -1;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    int k = matrix[i][j] - 'a';
                    a[k].Add((i, j));
                }

            SHeap<(int, int, int), int> hp = new((a, b) => a < b);
            hp.Add((0, 0, 0), 0);
            while (hp.Any())
            {
                ((int i, int j, int len), int step) = hp.Pop();
                if (len == mantra.Length) return step;
                if (len <= dp[i, j]) continue;
                else dp[i, j] = len;
                if (len > 0 && mantra[len] == mantra[len - 1])
                {
                    hp.Add((i, j, len + 1), step + 1);
                }
                else
                {
                    int next = mantra[len] - 'a';
                    foreach ((int ni, int nj) in a[next])
                    {
                        if (len + 1 > dp[ni, nj])
                            hp.Add((ni, nj, len + 1), step + Dist((i, j), (ni, nj)) + 1);
                    }
                }
            }
            return -1;
        }

        public int ExtractMantra_1(string[] matrix, string mantra)
        {
            List<(int, int)>[] a = new List<(int, int)>[26];
            for (int i = 0; i < 26; ++i) a[i] = new();
            int m = matrix.Length, n = matrix[0].Length;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    int k = matrix[i][j] - 'a';
                    a[k].Add((i, j));
                }
            (int, int) p = (0, 0);
            int ans = 0;
            foreach (int next in mantra.Select(c => c - 'a'))
            {
                if (!a[next].Any()) return -1;
                var p1 = (0, 0);
                int d = 999999, d1 = 0;
                foreach (var pn in a[next])
                    if ((d1 = Dist(pn, p)) < d)
                    {
                        d = d1;
                        p1 = pn;
                    }
                ans += d + 1;
            }
            return ans;
        }
        #endregion

        #region Problem D

        public string EvolutionaryRecord(int[] parents)
        {
            int n = parents.Length;
            string?[] dp = new string?[n];
            Dictionary<int, List<int>> di = new();
            for (int i = 0; i < n; ++i)
            {
                int p = parents[i];
                if (p >= 0)
                {
                    if (!di.ContainsKey(p)) di[p] = new();
                    di[p].Add(i);
                }
            }

            string DpDfs(int i)
            {
                if (dp[i] != null) return dp[i] ?? "";
                if (!di.ContainsKey(i) || !di[i].Any()) return dp[i] = "";
                List<string> li = new();
                foreach (var j in di[i])
                    li.Add($"0{DpDfs(j)}1");
                li.Sort();
                return dp[i] = string.Join("", li);
            }

            return DpDfs(0).TrimEnd('1');
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
            char p = 'C';
            LCP_202305 sln = new();

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
            int[] a1 = { 0, 3 }, a2 = { 4, 5 }, a3 = { 7, 9 };
            int[][] a = { a1, a2, a3 };

            return RampartDefensiveLine(a);
        }

        int RunTestC()
        {
            string[] sa = { "hydfpyqaoir", "ixpjveolliy", "hidhpqciygm", "icnefohovue", "qcwopbcbxbn", "dvahetjbfqg", "uiwjsukwofm", "spzjegbovxo", "aflruwmvkdp" };
            string s = "nrahvcpkmqgsyrcpmgfpmaxvydbp";
            return ExtractMantra(sa, s);
        }

        int RunTestD()
        {
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
