using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/9/11 US Daily
    // 2d-sort
    internal class P1383最大的团队表现值
    {
        public int MaxPerformance(int n, int[] speed, int[] efficiency, int k)
        {
            SortedSet<(int, int, int)> es = new();
            for (int i = 0; i < n; ++i)
                es.Add((efficiency[i], speed[i], i));
            SortedSet<(int, int)> sps = new();
            long sum = 0L, ma = 0L;
            while (es.Any())
            {
                (int e, int s, int i) = es.Max;
                sps.Add((s, i)); sum += s;
                es.Remove(es.Max);
                if (sps.Count > k)
                {
                    (int mi, _) = sps.Min;
                    sum -= mi;
                    sps.Remove(sps.Min);
                }
                ma = Math.Max(ma, sum * e);
            }
            return (int)(ma % 1000000007);
        }
    }
}
