using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1338数组大小减半
    {
        public int MinSetSize(int[] arr)
        {
            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
            foreach (var n in arr)
            {
                if (!dic.ContainsKey(n))
                {
                    dic[n] = new int[] { n, 1 };
                }
                else
                {
                    dic[n][1]++;
                }
            }
            int half = arr.Length >> 1;
            int count = 0, num = 0;
            foreach (var c in from pair in dic.Values
                              orderby pair[1] descending
                              select pair[1])
            {
                count += c;
                num++;
                if (count >= half)
                    return num; 
            }
            return num;
        }
    }
}
