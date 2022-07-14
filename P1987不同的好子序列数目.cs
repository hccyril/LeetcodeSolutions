using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/07/13
    // rank: 2422
    // DP
    internal class P1987不同的好子序列数目
    {
        public const string 类似题目 = "940. 不同的子序列 II";
        public int NumberOfUniqueGoodSubsequences(string binary)
        {
            if (!binary.Any(c => c == '0')) return binary.Length;
            int[] a = new int[binary.Length];
            int sum = 0, combo_i = 0, combo_sum = 0;
            for (int i = 0; i < binary.Length; ++i)
            {
                if (binary[i] == '0' && (i == 0 || a[i - 1] == 0)) continue;
                if (i == 0 || a[i - 1] == 0)
                {
                    sum = combo_sum = a[i] = 1;
                    combo_i = i;
                }
                else if (binary[i] == binary[i - 1])
                {
                    a[i] = a[i - 1];
                    sum = sum.Add(a[i]);
                    combo_sum = combo_sum.Add(a[i]);
                }
                else // binary[i] != binary[i - 1]
                {
                    a[i] = combo_sum;
                    if (combo_i > 0) a[i] = a[i].Add(a[combo_i - 1]);
                    sum = sum.Add(combo_sum = a[i]);
                    combo_i = i;
                }
                //Console.WriteLine("{0} {1} {2} {3} {4}", i, binary[i], a[i], sum, combo_sum);
            }
            return sum.Add(1);
        }

        internal static void Run()
        {
            // ans: 2
            string s1 = "001";
            // 31
            string s2 = "1101001";
            // 602367688
            string s3 = "00000010101010111111111110100000000000000010100000000000000001111011111111111111111111111101000010011011111";
            var sln = new P1987不同的好子序列数目();
            var ans = sln.NumberOfUniqueGoodSubsequences(s2);
            Console.WriteLine(nameof(P1987不同的好子序列数目) + ": " + ans);
        }
    }
}
