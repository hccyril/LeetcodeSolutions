using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, KMP算法
    // 注意这里的next算法才是花大功夫调好的（循环是有嵌套的！！），不要用回文子串那个例子
    // 好多问题，错了好多次，详见代码注释
    internal class P0686重复叠加字符串匹配
    {
        public int RepeatedStringMatch(string a, string b)
        {
            int[] next = new int[b.Length]; // next[i]表示最长后缀
            Array.Fill(next, -1);

            // 问题2：next数组的构建没弄好，没考虑到是嵌套的
            for (int i = 1; i < next.Length; ++i)
            {
                int nexti = next[i - 1] + 1;
                while (nexti >= 0 && b[nexti] != b[i])
                    nexti = nexti > 0 ? next[nexti - 1] + 1 : -1;
                next[i] = nexti;
            }
            // 附：之前写错的代码
            //for (int i = 1; i < next.Length; ++i)
            //{
            //    if (next[i - 1] >= 0 && b[next[i - 1] + 1] == b[i])
            //        next[i] = next[i - 1] + 1;
            //    else if (b[i] == b[0])
            //        next[i] = 0;
            //}

            int ia = 0, ib = 0, count = 0, repeat = 1;
            while (count < b.Length)
            {
                if (ia >= a.Length)
                {
                    ++repeat;
                    ia -= a.Length;
                }

                // 问题3：边界条件没设定好，存在一个用例使得count永远不会等于0，然后超时了
                if ((repeat - 1) * a.Length + ia + 1 > a.Length + b.Length) return -1;
                //if (repeat > 1 && count == 0) return -1; // wrong!

                // Console.WriteLine($"a[{ia}] b[{ib}] {a[ia]} vs {b[ib]}"); // DEBUG

                // 问题1：下标没处理好，ia算错了
                if (a[ia] == b[ib])
                {
                    ++count; ++ia; ++ib;
                }
                else if (ib > 0) ib = count = next[ib - 1] + 1;
                else ++ia;
            }
            return repeat;
        }

        internal static void Run()
        {
            //string a = "aaac", b = "aac";
            string a = "aabaaabaaac", b = "aabaaac";
            Console.WriteLine(new P0686重复叠加字符串匹配().RepeatedStringMatch(a, b));
        }
    }
}
