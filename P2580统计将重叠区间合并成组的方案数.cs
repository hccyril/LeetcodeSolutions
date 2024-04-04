using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// Medium, 2024/3/27 Daily
// range set
internal class P2580统计将重叠区间合并成组的方案数
{
    public int CountWays(int[][] ranges)
    {
        SortedSet<Interval> ss = new();
        foreach (var r in ranges)
        {
            // 不能直接套merge方法，原因是查询不需要(start-1, end+1), 例如[1,3]和[4,5]并不相连
            var ra = new Interval() { start = r[0], end = r[1] };
            while (ss.TryGetValue(ra, out var r0))
            {
                ss.Remove(r0);
                (ra.start, ra.end) = (Math.Min(r0.start, ra.start), Math.Max(r0.end, ra.end));
            }
            ss.Add(ra);
        }
        return 2.PowerMod(ss.Count, 1000000007);
    }
}
