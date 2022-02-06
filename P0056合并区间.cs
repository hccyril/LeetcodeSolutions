using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0056合并区间
    {
        public int[][] Merge(int[][] intervals)
        {
            List<int[]> list = new List<int[]>();
            Heap<int[]> hp = new Heap<int[]>((a, b) => a[0] < b[0]);
            foreach (var p in intervals) hp.Push(p);
            int[] r = hp.Pop(); list.Add(r);
            while (hp.Count > 0)
            {
                var r1 = hp.Pop();
                if (r1[0] <= r[1])
                {
                    if (r1[1] > r[1]) r[1] = r1[1];
                }
                else
                {
                    list.Add(r = r1);
                }
            }
            return list.ToArray();
        }
    }
}
