using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/7/12 practice
    // 练习失败，截至2023/7/12 17:30 已经花了2小时18分还未做出来
    // 2023/7/14 6:47 AC, 历时5小时以上，WA了5次，神坑！
    internal class P1585检查字符串是否可以通过排序子字符串得到另一个字符串
    {
        // ver5 - AC
        // 最后想到counter要枚举每个数字，比方说8之前要有2个3，1个4，那t中选到8时就要看至少有2个3，1个4
        public bool IsTransformable(string s, string t)
        {
            int[,] ca = new int[s.Length, 10];
            int[] cn = new int[10];

            Queue<int>[] qa = new Queue<int>[10];
            for (int i = 0; i < 10; ++i) qa[i] = new();

            for (int i = 0; i < s.Length; ++i)
            {
                int d = s[i] - '0';
                for (int j = 0; j < d; ++j)
                    ca[i, j] = cn[j];
                ++cn[d];
                qa[d].Enqueue(i);
            }

            Array.Fill(cn, 0);
            for (int i = 0; i < t.Length; ++i)
            {
                int d = t[i] - '0';
                if (!qa[d].Any())
                    return false;
                int j = qa[d].Dequeue();
                for (int k = 0; k < d; ++k)
                    if (cn[k] < ca[j, k]) return false;

                ++cn[d];
            }

            return true;
        }

        // ver3改=ver4，依然WA!
        public bool IsTransformable_WA4(string s, string t)
        {
            Queue<(int, int)>[] qa = new Queue<(int, int)>[10];
            for (int i = 0; i < 10; ++i) qa[i] = new();
            int[] ps = new int[10];
            for (int i = 0; i < s.Length; ++i)
            {
                int d = s[i] - '0';
                qa[d].Enqueue((ps[d], i));
                for (; d < 10; ++d)
                    ps[d]++;
            }
            int c = 0;
            Span<int> ca = stackalloc int[s.Length];
            for (int i = 0; i < t.Length; ++i)
            {
                c += ca[i];
                int d = t[i] - '0';
                if (!qa[d].Any() || qa[d].Peek().Item1 > c) 
                    return false;
                (int p, int j) = qa[d].Dequeue();
                if (j <= i) ++c;
                else ++ca[j];
            }
            return true;
        }

        // 使用队列，但还是WA: "891", "198"
        public bool IsTransformable_WA3(string s, string t)
        {
            Queue<int>[] qa = new Queue<int>[10];
            for (int i = 0; i < 10; ++i) qa[i] = new();
            int[] ps = new int[10];
            foreach (var c in s)
            {
                int d = c - '0';
                qa[d].Enqueue(ps[d]);
                for (; d < 10; ++d)
                    ps[d]++;
            }
            for (int i = 0; i < t.Length; ++i)
            {
                int d = t[i] - '0';
                if (!qa[d].Any() || qa[d].Peek() > i) return false;
                qa[d].Dequeue();
            }
            return true;
        }

        static readonly int[] ra = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

        // 2023/7/13再重写
        // 还是WA，似乎整个思路都错了
        public bool IsTransformable_WA2(string s, string t)
        {
            int[] cL = new int[10], cR = new int[10];
            int[] sa = s.Select(c => c - '0').ToArray(),
                ta = t.Select(c => c - '0').ToArray();

            // lo / hi 左边counter的最大值/右边counter的最小值
            // left / right 左右counter扫描到的边界指针
            // i, j 左右两边第一个还未转换的下标位置
            int lo = -1, hi = 10, left = -1, right = s.Length, i = 0, j = s.Length - 1;

            // (1) left -> right
            while (i <= j)
            {
                while (left + 1 < right && sa[left + 1] <= hi)
                    ++cL[hi = sa[++left]];
                if (cL[ta[i]] <= 0) break;
                while (i <= j && cL[ta[i]] > 0)
                    --cL[ta[i++]];
                hi = Enumerable.Range(0, 10).FirstOrDefault(i => cL[i] > 0);
            }

            // (2) right -> left
            while (i <= j)
            {
                while (right - 1 > left && sa[right - 1] >= lo)
                    ++cR[lo = sa[--right]];
                if (cR[ta[j]] <= 0) break;
                while (j >= i && cR[ta[j]] > 0)
                    --cR[ta[j--]];
                lo = ra.FirstOrDefault(j => cR[j] > 0);
            }

            if (i > j) return true;

            // 两边打通的情况
            if (left + 1 == right && lo <= hi)
            {
                for (int k = 0; k < 10; ++k)
                    cL[k] += cR[k];
                for (int k = i; k <= j; ++k)
                    if (cL[ta[k]] <= 0)
                        return false;
                    else
                        --cL[ta[k]];
                return true;
            }
            return false;
        }

        // ver1 - WA
        public bool IsTransformable_WA1(string s, string t)
        {
            int[] cL = new int[10], cR = new int[10];
            int[] sa = s.Select(c => c - '0').ToArray(),
                ta = t.Select(c => c - '0').ToArray();

            // lo / hi 左边counter的最大值/右边counter的最小值
            // left / right 左右counter扫描到的边界指针
            // i, j 左右两边第一个还未转换的下标位置
            int lo = -1, hi = 10, left = -1, right = s.Length, i = 0, j = s.Length - 1;
            while (i <= j)
            {
                int before = j - i;

                // 两边打通的情况
                if (left + 1 == right && lo <= hi)
                {
                    for (int k = 0; k < 10; ++k)
                        cL[k] += cR[k];
                    for (int k = i; k <= j; ++k)
                        if (cL[sa[k]] <= 0)
                            return false;
                        else
                            --cL[sa[k]];
                    return true;
                }

                // left -> right
                while (i <= j)
                {
                    while (left + 1 < right && ta[left + 1] >= lo)
                        ++cL[lo = ta[++left]];
                    if (cL[sa[i]] <= 0) break;
                    while (i <= j && cL[sa[i]] > 0)
                        --cL[sa[i++]];
                    lo = ra.FirstOrDefault(i => cL[i] > 0);
                }

                // right -> left
                while (i <= j)
                {
                    while (right - 1 > left && ta[right - 1] <= hi)
                        ++cR[hi = ta[--right]];
                    if (cR[sa[j]] <= 0) break;
                    while (j >= i && cR[sa[j]] > 0)
                        --cR[sa[j--]];
                    hi = Enumerable.Range(0, 10).FirstOrDefault(j => cR[j] > 0);
                }

                int after = j - i;
                if (before == after)
                    return false;
            }

            return true;
        }
        // 越写越乱，重写
        //public bool IsTransformable(string s, string t)
        //{
        //    int[] cL = new int[10], cR = new int[10];
        //    int[] ta = t.Select(c => c - '0').ToArray();
        //    cL[ta[0]] = cR[ta[^1]] = 1;
        //    int iL = 0, iR = ta.Length - 1, l = 0, r = ta.Length - 1;
        //    while (iL < iR)
        //    {
        //        int before = iR - iL;

        //        // left -> right
        //        int h = ra.FirstOrDefault(i => cL[i] > 0);
        //        while (l + 1 < r)
        //        {
        //            if (l + 1 < r && ta[l + 1] >= h)
        //            {
        //                ++cL[ta[++l]];
        //                h = ta[l];
        //            }
        //            else
        //                break;
        //        }
        //        while (iL < s.Length && cL[s[iL] - '0'] > 0)
        //        {
        //            --cL[s[iL] - '0'];
        //            ++iL;
        //        }

        //        // right -> left
        //        h = Enumerable.Range(0, 10).FirstOrDefault(i => cR[i] > 0);

        //    }
        //    return true;
        //}
        // WA-bak
        //public bool IsTransformable(string s, string t)
        //{
        //    int[] cn = new int[10];
        //    cn[t[0] - '0']++;
        //    int i = 0, j = 0;
        //    while (i < s.Length)
        //    {
        //        int h = ra.FirstOrDefault(i => cn[i] > 0);
        //        while (j < t.Length)
        //        {
        //            if (j + 1 < t.Length && t[j + 1] - '0' >= h)
        //            {
        //                ++cn[t[++j] - '0'];
        //                h = t[j] - '0';
        //            }
        //            else
        //                break;
        //        }
        //        if (cn[s[i] - '0'] <= 0) return false;
        //        while (i < s.Length && cn[s[i] - '0'] > 0)
        //        {
        //            --cn[s[i] - '0'];
        //            ++i;
        //        }
        //    }
        //    return true;
        //}

        internal static void Run()
        {
            //string s = "84532", t = "34852"; // true

            // almost the last case - ans=true
            // simplify the above case
            //string s = "999888777666555444333222111000", t = "012345678901234567890123456789";

            // WA again (should be true) 
            //string s = "432513576", t = "231435567";

            // WA again (should be true)
            //string s = "999888777666555444333222111000", t = "111000222333444555666777888999";

            // WA again (should be false)
            string s = "848188", t = "818884";

            var sln = new P1585检查字符串是否可以通过排序子字符串得到另一个字符串();
            var ans = sln.IsTransformable(s, t);
            Console.WriteLine("ans={0}", ans);
        }
    }
}
