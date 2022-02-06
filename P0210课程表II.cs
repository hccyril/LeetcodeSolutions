using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 最典型的拓扑排序
    internal class P0210课程表II
    {
        class Course
        {
            public int id;
            public HashSet<int> pres = new(); // 本门课的先修课程
            public List<int> nexts = new(); // 本门课的后修课程
        }
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            Course[] arr = new Course[numCourses];
            for (int i = 0; i < numCourses; i++)
                arr[i] = new Course() { id = i };
            foreach (var p in prerequisites)
            {
                arr[p[0]].pres.Add(p[1]);
                arr[p[1]].nexts.Add(p[0]);
            }
            Queue<Course> qu = new();
            foreach (var c in arr.Where(t => !t.pres.Any()))
                qu.Enqueue(c);
            int[] ans = new int[numCourses];
            int count = 0;
            while (qu.Any())
            {
                var c = qu.Dequeue();
                ans[count++] = c.id;
                foreach (var nc in c.nexts)
                {
                    arr[nc].pres.Remove(c.id);
                    if (!arr[nc].pres.Any())
                        qu.Enqueue(arr[nc]);
                }
            }

            return count == numCourses ? ans : Array.Empty<int>();
        }
    }
}
