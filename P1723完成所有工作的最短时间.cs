using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/17
    // rank: 2284
    // dijkstra
    internal class P1723完成所有工作的最短时间
    {
        public int MinimumTimeRequired(int[] jobs, int k)
        {
            // ver2 关于用一个排序把一个超时问题解决了这件事
            Array.Sort(jobs);
            Array.Reverse(jobs);

            SHeap<long, int> hp = new((a, b) => a < b, true);
            hp.Add(0L, 0);
            int[] workers = new int[k];
            while (hp.Any())
            {
                (long map, int time) = hp.Pop();
                int n = (int)(map & 0xFL);
                if (n == jobs.Length) return time;

                // unzip state
                Array.Fill(workers, 0);
                for (int i = 1; i <= n; ++i)
                {
                    int t = jobs[i - 1], p = (int)((map >> (i << 2)) & 0xFL);
                    workers[p] += t;
                }

                // put the (n+1)th job
                for (int a = k - 1; a >= 0; --a)
                {
                    int t = time;
                    if (a > 0 && workers[a] == 0 && workers[a - 1] == 0)
                        continue;
                    t = Math.Max(t, workers[a] += jobs[n]);

                    // zip state and push heap
                    long m = map + 1L | ((long)a << (n + 1 << 2));
                    hp.Add(m, t);
                    
                    workers[a] -= jobs[n];
                }
            }
            return -1;
        }

        internal static void Run()
        {
            // ver1 TLE
            int[] jobs = { 10001, 10002, 10003, 10004, 10005, 10006, 10007, 10008, 10009, 10010, 10011, 1000000 };
            int k = 12;

            var sln = new P1723完成所有工作的最短时间();
            int ans = sln.MinimumTimeRequired(jobs, k);
            Console.WriteLine(ans);
        }
    }
}
