using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/1/24 daily
    internal class P2045到达目的地的第二短时间
    {
        // ver1(BFS) - Out of memory
        // ver2: added visited setlist, second should be at most (first + 2) - WA
        // ver3: each site should be able to visit at most 2 times with different t (but may with same t many times) - AC
        public int SecondMinimum(int n, int[][] edges, int time, int change)
        {
            List<int>[] vs = new List<int>[n];
            int[] visited = new int[n];
            for (int i = 0; i < n; ++i) vs[i] = new();
            foreach (var ed in edges)
            {
                int a = ed[0] - 1, b = ed[1] - 1;
                vs[a].Add(b); vs[b].Add(a);
            }
            Queue<(int s, int t)> qu = new();
            qu.Enqueue((0, 0)); visited[0] = -1;
            int first = 0;
            while (qu.Any())
            {
                (int s, int t) = qu.Dequeue(); ++t;
                foreach (var next in vs[s])
                {
                    if (visited[next] < 0) continue;
                    if (visited[next] == 0)
                        visited[next] = t;
                    else if (visited[next] + 1 == t)
                        visited[next] = -t;
                    else continue;
//#if DEBUG
//                    Console.WriteLine($"next={next} t={t}"); // DEBUG
//#endif
                    if (next == n - 1)
                    {
                        if (first == 0) first = t;
                        else if (t > first) return CalcTime(t, time, change);
                    }
                    if (first > 0 && t > first + 1)
                    {
                        qu.Clear();
                        break;
                    }
                    qu.Enqueue((next, t));
                }
            }
            return CalcTime(first + 2, time, change);
        }

        private int CalcTime(int travels, int time, int change)
        {
            int t = 0;
            while (travels-- > 0)
            {
                if (((t / change) & 1) != 0) t += change - t % change;
                t += time;
            }
            return t;
        }

        internal static void Run()
        {
            // expected: 13
            var input = new P2045InputData
            {
                n = 5,
                edges = new int[][] { new int[] { 1, 2 }, new int[] { 1, 3 }, new int[] { 1, 4 }, new int[] { 3, 4 }, new int[] { 4, 5 } },
                time = 3,
                change = 5
            };

            // big data
            //var input = Common.ReadInput<P2045InputData>(2045);

            var ans = new P2045到达目的地的第二短时间().SecondMinimum(input.n, input.edges, input.time, input.change);
            Console.WriteLine("SecondMinimum=" + ans);
        }
    }

    class P2045InputData
    {
        public int n { get; set; }
        public int[][] edges { get; set; }
        public int time { get; set; }
        public int change { get; set; }
    }
}
