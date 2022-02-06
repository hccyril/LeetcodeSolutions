using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0791自定义字符串排序
    {
        public string CustomSortString(string order, string str)
        {
            int index = 1;
            int[] arr = new int[26];
            foreach (var c in order)
                arr[c - 'a'] = index++;

            List<char> list = str.ToList();
            list.Sort((c1, c2) => arr[c1 - 'a'] - arr[c2 - 'a']);
            return new string(list.ToArray());
        }
    }
}
