using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0954二倍数对数组
    {
        public bool CanReorderDoubled(int[] arr)
        {
            SortedList<int, List<int>> sl = new SortedList<int, List<int>>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (var n in arr)
            {
                int key = Math.Abs(n);
                if (!sl.ContainsKey(key)) sl.Add(key, new List<int>());
                sl[key].Add(n);
                if (!dic.ContainsKey(n)) dic.Add(n, 1);
                else dic[n]++;
            }
            foreach (var kv in sl)
                foreach (var n in kv.Value)
                    if (dic[n]-- > 0)
                        if (!dic.ContainsKey(n * 2) || dic[n * 2]-- <= 0) 
                            return false;
            return true;
        }
    }
}
