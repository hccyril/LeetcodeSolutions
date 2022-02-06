using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    internal class P0630课程表III
    {
        void 说明()
        {
            var 类似题 = new P1353最多可以参加的会议数目();
        }

        public int ScheduleCourse(int[][] courses)
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
}
