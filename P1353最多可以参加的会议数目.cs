using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 巨难的贪心法（据说是2021/10/14 B站算法类笔试题）
    // 2021/11/21
    class P1353最多可以参加的会议数目
    {
        string 相关题1 = nameof(P1235规划兼职工作);
        string 相关题2 = nameof(P1751最多可以参加的会议数目II);

        // ver3: 在ver2的基础上改进优化 -> AC(50%)
        // 因为最终超时的case是[[1,1],[1,2],[1,3],...[1,100000]]
        // 针对这个case做优化即可通过
        public int MaxEvents(int[][] events)
        {
            int n = events.Length;
            int count = 0;
            var arr = events.Select(e => new
            {
                start = e[0],
                end = e[1]
            }).OrderBy(t => t.end).ThenBy(t => t.start).ToArray();
            HashSet<int> hs = new();
            int start = 1;
            foreach (var a in arr)
            {
                int d = Math.Max(start, a.start);
                while (d <= a.end && !hs.Add(d)) d++;
                if (d <= a.end) count++;
                if (d == start) start++;
            }
            return count;
        }

        // ver2: 超时
        // 说明：day[n]=i : 表示我最早能在前i天完成n个会议, 最终day是个参加会议的日期数组（排好序的）
        // 每一次搜索我都找到这次能参加nd场会议，而参加nd场会议的条件是在attend天完成
        // 所以最终整个过程相当于做了一次插入排序
        //public int MaxEvents(int[][] events)
        //{
        //    int n = events.Length;
        //    int count = 0;
        //    int[] day = new int[n];
        //    var arr = events.Select(e => new
        //    {
        //        start = e[0],
        //        end = e[1]
        //    }).OrderBy(t => t.end).ThenBy(t => t.start).ToArray();
        //    HashSet<int> hs = new();
        //    foreach (var a in arr)
        //    {
        //        int d = a.start;
        //        while (d <= a.end && !hs.Add(d)) d++;
        //        if (d <= a.end)
        //        {
        //            day[count++] = d;
        //            Array.Sort(day, 0, count);
        //        }
        //    }
        //    return count;
        //}

        // ver1 没考虑清楚，失败
        //if (count == 0) day[count++] = a.start;
        //else
        //{
        //    for (int d = count - 1; d >= 0; --d)
        //    {
        //        if (day[d] < a.end)
        //        {
        //            int nd = d + 1, attend = Math.Max(day[d] + 1, a.start);
        //            if (nd < count)
        //            {
        //                if (day[nd] > attend) day[nd] = attend;
        //            }
        //            else
        //            {
        //                day[count++] = attend;
        //            }
        //            break;
        //        }
        //    }
        //}
    }
}
