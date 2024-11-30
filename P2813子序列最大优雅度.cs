using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/6/13 Daily
// rating: 2582
internal class P2813子序列最大优雅度
{
    public long FindMaximumElegance(int[][] items, int k)
    {
        int n = items.Length, i = 0;
        long sm = 0L, maxev = 0L;
        HashSet<int> hs = new();
        List<long> popList = new();
        int popCount = 0;
        foreach (var p in items.OrderByDescending(t => t[0]))
        {
            long pf = p[0]; int cat = p[1];
            if (i < k)
            {
                sm += pf;
                if (!hs.Add(cat))
                {
                    popList.Add(pf);
                }
                long c = hs.Count;
                maxev = sm + c * c;
            }
            else if (popCount < popList.Count)
            {
                if (i == k) popList.Sort();

                if (hs.Add(cat))
                {
                    sm -= popList[popCount++];
                    sm += pf;

                    long c = hs.Count;
                    maxev = Math.Max(maxev, sm + c * c);
                }
            }
            else break;
            ++i;
        }
        return maxev;
    }
}
