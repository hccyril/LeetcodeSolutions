using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // BFS (as n <= 15)
    internal class P1494并行课程II
    {
        // 尝试使用stackalloc，但代价是用不了yield return和Aggregate
        List<int> MakePlan(int[] pr, int taken, int k)
        {
            var available = Enumerable.Range(0, pr.Length)
                .Where(i => ((1 << i) & taken) == 0 && (pr[i] & taken) == pr[i])
                .ToList();
            //List<int> available = new();
            //foreach (int i in Enumerable.Range(0, pr.Length))
            //    if ((pr[i] & taken) == pr[i]) available.Add(i);
            if (available.Count <= k) return new() { available.Aggregate(0, (sum, a) => sum |= 1 << a) };
            else
            {
                List<int> ans = new();
                Span<int> plan = stackalloc int[k];
                plan.Fill(-1); plan[0] = 0;
                int i = 1;
                while (i >= 0)
                {
                    if (i == k)
                    {
                        int map = 0;
                        foreach (int p in plan)
                            map |= 1 << available[p];
                        ans.Add(map);
                        // yield return map;
                        // yield return plan.Aggregate((sum, index) => sum |= 1 << available[index]);
                        --i;
                    }
                    else if (plan[i] < 0)
                    {
                        plan[i] = plan[i - 1] + 1;
                        ++i;
                    }
                    else
                    {
                        if (++plan[i] + k - i > available.Count)
                        {
                            plan[i] = -1;
                            --i;
                        }
                        else ++i;
                    }
                }
                return ans;
            }
        }
        public int MinNumberOfSemesters(int n, int[][] relations, int k)
        {
            int[] pr = new int[n];
            foreach (var re in relations)
            {
                int pre = re[0] - 1, cr = re[1] - 1;
                pr[cr] |= 1 << pre;
            }
            HashSet<int> hs = new();
            Queue<int> qu = new();
            qu.Enqueue(0);
            int full = (1 << n) - 1;
            while (qu.Any())
            {
                int key = qu.Dequeue(), sem = (key >> 15) + 1, taken = key & 32767;
                foreach (int plan in MakePlan(pr, taken, k))
                {
                    if ((taken | plan) == full) return sem;
                    key = (sem << 15) | taken | plan;
                    if (hs.Add(key))
                        qu.Enqueue(key);
                }
            }
            return -1;
        }

        internal static void Run()
        {
            var sln = new P1494并行课程II();
            int n = 4, k = 2;
            var dependencies = new int[][] { new int[] { 2, 1 }, new int[] { 3, 1 }, new int[] { 1, 4 } };
            var result = sln.MinNumberOfSemesters(n, dependencies, k);
            Console.WriteLine("sem=" + result);
        }
    }
}
