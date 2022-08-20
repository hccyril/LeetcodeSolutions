using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/7/19 Daily
    internal class P0731我的日程安排表II
    {
        SortedSet<Interval> si1 = new(), si2 = new();
        public bool Book(int start, int end)
        {
            Interval ra = new() { start = start, end = end - 1 };
            if (si2.Contains(ra)) return false;
            while (si1.TryGetValue(ra, out var r1))
            {
                si1.Remove(r1);
                si2.Add(new() { start = Math.Max(r1.start, ra.start), end = Math.Min(r1.end, ra.end) });
                (ra.start, ra.end) = (Math.Min(r1.start, ra.start), Math.Max(r1.end, ra.end));
            }
            return si1.Add(ra);
        }
    }
}
