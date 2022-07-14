using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, OOP, 2021/10/09 Daily
    // 2022/7/12重做
    class P0352将数据流变为多个不相交区间
    {
        SortedSet<Interval> sort = new();
        public void AddNum(int val)
        {
            if (sort.Contains(new(val))) return; // 注意考虑重复添加的情况
            sort.MergeOne(new(val));
        }

        public int[][] GetIntervals()
            => sort.Select(r => new[] { r.start, r.end }).ToArray();
    }
    /* Test Cases
    ["SummaryRanges","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals"]
    [[],[1],[],[3],[],[7],[],[2],[],[6],[]]

    ["SummaryRanges","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals","addNum","getIntervals"]
    [[],[6],[],[6],[],[0],[],[4],[],[8],[],[7],[],[6],[],[4],[],[7],[],[5],[]]
     * */
}
