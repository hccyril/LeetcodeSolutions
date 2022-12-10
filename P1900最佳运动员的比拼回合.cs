using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/9/11
    // rank: 2454
    // BFS
    internal class P1900最佳运动员的比拼回合
    {
        public int[] EarliestAndLatest(int n, int firstPlayer, int secondPlayer)
        {
            Dictionary<(int, int, int), int> dic = new();
            int l = firstPlayer - 1, m = secondPlayer - firstPlayer - 1, r = n - secondPlayer;
            if (l == r) return new int[] { 1, 1 };
            if (l > r) (l, r) = (r, l);
            dic[(l, m, r)] = 1;
            Queue<(int, int, int)> qu = new();
            qu.Enqueue((l, m, r));

            IEnumerable<(int, int, int)> PermuteNext(int l, int m, int r)
            {
                int rr = r - l - 1;
                for (int nl = 0; nl <= l; ++nl)
                {
                    //int ra = r - nl - 1;
                    if (m == rr)
                    {
                        for (int nm = 0; nm <= m; ++nm)
                        {
                            int nr = r - nl - 1 - nm;
                            yield return nl > nr ? (nr, nm, nl) : (nl, nm, nr);
                        }
                    }
                    else if (m < rr)
                    {
                        int rrr = rr - m - 1 >> 1;
                        for (int nm = 0; nm <= m; ++nm)
                        {
                            int nr = r - nl - 1 - nm - 1 - rrr;
                            yield return nl > nr ? (nr, nm, nl) : (nl, nm, nr);
                        }
                    }
                    else // mi > rr
                    {
                        int mr = m - rr - 1 >> 1;
                        for (int ra = 0; ra <= rr; ++ra)
                        {
                            int nr = l - nl + ra;
                            int nm = m - ra - 1 - mr;
                            yield return nl > nr ? (nr, nm, nl) : (nl, nm, nr);
                        }
                        //for (int nr = 0; nr <= rr; ++nr)
                        //{
                        //    int nm = m - nr - 1 - mr;
                        //    yield return nl > nr ? (nr, nm, nl) : (nl, nm, nr);
                        //}
                    }
                }
            }

            int mi = n + 1, ma = -1;
            while (qu.Any())
            {
                (l, m, r) = qu.Dequeue();
                int p = dic[(l, m, r)];
                Console.WriteLine("{0} {1} {2} {3}", l, m, r, p);
                foreach ((int nl, int nm, int nr) in PermuteNext(l, m, r))
                {
                    if (nl == nr)
                    {
                        mi = Math.Min(mi, p + 1);
                        ma = Math.Max(ma, p + 1);
                    }
                    else if (!dic.ContainsKey((nl, nm, nr)))
                    {
                        dic[(nl, nm, nr)] = p + 1;
                        qu.Enqueue((nl, nm, nr));
                    }
                }
            }
            return new int[] { mi, ma };
        }

        internal static void Run()
        {
            Console.WriteLine(string.Join(" ", new P1900最佳运动员的比拼回合()
                .EarliestAndLatest(12, 6, 8))); // 2, 4
        }
    }
}
