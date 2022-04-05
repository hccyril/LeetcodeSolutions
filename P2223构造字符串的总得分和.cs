using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P2223构造字符串的总得分和
    {

        public long SumScores(string s)
        {
            int[] next = new int[s.Length]; 
            Array.Fill(next, -1);

            for (int i = 1; i < next.Length; ++i)
            {
                int nexti = next[i - 1] + 1;
                while (nexti >= 0 && s[nexti] != s[i])
                    nexti = nexti > 0 ? next[nexti - 1] + 1 : -1;
                next[i] = nexti;
            }

            int[] arr = new int[s.Length];
            Array.Fill(arr, -1);
            for (int i = 0; i < s.Length; ++i)
            {
                if (arr[i] < 0)
                {
                    int i0 = i, j = 0;
                    while (i0 >= 0 && i0 < s.Length && arr[i0] < 0)
                    {
                        while (i0 + j < s.Length && s[i0 + j] == s[j]) ++j;
                        arr[i0] = j;

                        if (j == 0) break;
                        else
                        {
                            int i1 = i0 + j - 1;
                            j = next[j - 1];
                            if (j < 0) break;
                            i0 = i1 - j;
                        }
                    }
                }
            }

            long sum = 0L;
            //int ind = s.Length - 1;
            //sum += s.Length;
            //while (ind >= 0 && next[ind] >= 0)
            //{
            //    sum += next[ind] + 1L;
            //    ind = next[ind];
            //}
            //foreach (var n in next) Console.WriteLine(n);
            foreach (var n in arr) sum += n;
            return sum;
        }

        internal static void Run()
        {
            string s = "azbazbzaz"; // 14
            // "babab"; // 9
            var sln = new P2223构造字符串的总得分和();
            Console.WriteLine(sln.SumScores(s));
        }
    }
}
