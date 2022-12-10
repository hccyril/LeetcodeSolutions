using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/10/07
    // 区间算法
    class P2276_CountIntervals
    {
        SortedSet<Interval> ss = new();
        int cnt = 0;

        public void Add(int left, int right)
        {
            Interval r = new() { start = left, end = right };
            while (ss.TryGetValue(r, out var ar)) {
                ss.Remove(ar);
                cnt -= ar.Count;
                r.start = Math.Min(r.start, ar.start);
                r.end = Math.Max(r.end, ar.end);
            }
            ss.Add(r);
            cnt += r.Count;
        }

        public int Count() => cnt;
    }

    internal class P2276统计区间中的整数数目
    {
    }
}
