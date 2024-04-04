using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2021/12/14
// 2024/2/9 补做反悔贪心法
internal class P0630课程表III
{
    // 反悔贪心（or 加油贪心 as 871）
    // 当某个课程无法选择时，看是否能在已经选了的课程里退掉一门最花时间的，然后让该门课选上
    public int ScheduleCourse(int[][] courses)
    {
        int ds = 0, ans = 0;
        PriorityQueue<int, int> pq = new();
        foreach (var cp in courses.OrderBy(t => t[1]))
        {
            int d = cp[0], e = cp[1];
            if (ds + d <= e)
            {
                ds += d;
                pq.Enqueue(d, -d);
                ++ans;
            }
            else if (pq.TryPeek(out int d0, out _) && d0 > d)
            {
                pq.EnqueueDequeue(d, -d);
                ds = ds - d0 + d;
            }
        }
        return ans;
    }

    // ver1 二分
    // ref P1353最多可以参加的会议数目
    public int ScheduleCourse_ver1(int[][] courses)
    {
        var courseList = courses
            .Where(t => t[0] <= t[1])
            .Select(t => new { dur = t[0], last = t[1] })
            .OrderBy(t => t.last)
            .ToList();
        int[] arr = new int[courseList.Count + 1]; // arr[i] 表示修完i门课所花的最少时间
        int count = 0;
        foreach (var cr in courseList)
        {
            int start = cr.last - cr.dur;
            int i = Array.BinarySearch(arr, 0, count + 1, start);
            i = i < 0 ? -i - 1 : i + 1; // i 是插入的位置，根据二分搜索结果进行调整
            if (i > count) count = i;
            for (; i >= 1; --i)
            {
                int finish = arr[i - 1] + cr.dur;
                if (arr[i] == 0 || arr[i] > finish)
                    arr[i] = finish;
            }
        }
        return count;
    }

    internal static void Run()
    {
        int[] c1 = { 1, 2 }, c2 = { 2, 3 };
        int[][] cs = { c1, c2 };
        Console.WriteLine(new P0630课程表III().ScheduleCourse(cs));
    }
}
