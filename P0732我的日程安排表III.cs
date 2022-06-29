using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/6 Daily
    internal class P0732我的日程安排表III
    {
        SortedDictionary<int, int> sd = new();
        int Set(int t, int i) => sd.ContainsKey(t) ? sd[t] += i : sd[t] = i;
        public int Book(int start, int end)
        {
            Set(start, 1);
            Set(end, -1);
            int k = 0, max_k = 0;
            foreach (var i in sd.Values)
                max_k = Math.Max(max_k, k += i);
            return max_k;
        }
    }
}
