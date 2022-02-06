using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1743从相邻元素对还原数组
    {
        public int[] RestoreArray(int[][] adjacentPairs)
        {
            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
            foreach (var pr in adjacentPairs)
            {
                for (int i = 0; i < pr.Length; ++i)
                {
                    int a = pr[i], b = pr[1 - i];
                    if (!dic.ContainsKey(a)) dic.Add(a, new int[] { -100001, -100001 });
                    if (dic[a][0] < -100000) dic[a][0] = b;
                    else dic[a][1] = b;
                }
            }
            int num = -100000;
            foreach (var key in dic.Keys)
            {
                if (dic[key][0] < -100000 || dic[key][1] < -100000)
                {
                    num = key;
                    break;
                }
            }
            int[] ans = new int[dic.Count];
            for (int i = 0; i < ans.Length; ++i)
            {
                ans[i] = num;
                int num2 = dic[num][0];
                if (num2 < -100000 || i > 0 && num2 == ans[i - 1])
                    num2 = dic[num][1];
                num = num2;
            }
            return ans;
        }

        public static void Run()
        {
            int[] a = { 2, 1 }, b = { 3, 4 }, c = { 3, 2 };
            int[][] input = { a, b, c };
            var output = new P1743从相邻元素对还原数组().RestoreArray(input);
            Console.WriteLine("[" + string.Join(',', output) + "]");
        }
    }
}
