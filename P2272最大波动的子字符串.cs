using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, BiC78-D, 2022/5/14
    // start: 0:08:26 end: 1:16:54, spent: 1:08:28
    internal class P2272最大波动的子字符串
    {
        // D
        public int LargestVariance(string s)
        {
            int[] arr = new int[26];
            if (!QuickPass(s, arr)) return 0;
            int ans = 0;
            for (int i = 0; i < 26; ++i)
                for (int j = 0; j < 26; ++j)
                    if (i != j && arr[i] > 0 && arr[j] > 0)
                        ans = Math.Max(ans, GetVariance(s, (char)(i + 'a'), (char)(j + 'a')));
            return ans;
        }

        int GetVariance(string s, char a, char b)
        {
            int diff = 0, na = 0, nb = 0;
            bool isFirstA = false;
            foreach (var c in s)
            {
                if (c == a)
                {
                    //// 比赛后的优化 - but WRONG
                    //if (++na > nb)
                    //{
                    //    na = 1;
                    //    nb = 0;
                    //    isFirstA = true;
                    //}
                    //else
                    //{
                    //    if (isFirstA)
                    //    {
                    //        --na;
                    //        isFirstA = false;
                    //    }
                    //    diff = Math.Max(diff, nb - na);
                    //}
                    // 比赛时写的逻辑：it's ugly but correct
                    if (++na == 1 && nb == 0)
                        isFirstA = true;
                    else if (na > 1)
                    {
                        if (nb > 0 && isFirstA)
                        {
                            na--;
                            isFirstA = false;
                        }
                        else if (nb == 0)
                        {
                            na = 1;
                            isFirstA = true;
                        }
                        if (na > nb)
                        {
                            na = 1;
                            nb = 0;
                            isFirstA = true;
                        }
                    }
                    if (nb > 0)
                        diff = Math.Max(diff, nb - na);
                }
                else if (c == b)
                {
                    ++nb;
                    if (na > 0)
                        diff = Math.Max(diff, nb - na);
                }
            }
            return diff;
        }

        bool QuickPass(string s, int[] arr)
        {
            bool allSame = true;
            foreach (var c in s)
            {
                if (allSame && c != s[0]) allSame = false;
                arr[c - 'a']++;
            }
            return !allSame && arr.Any(i => i > 1);
        }

        internal static void Run()
        {
            var sln = new P2272最大波动的子字符串();
            int ans = sln.LargestVariance("bbc");
            Console.WriteLine(ans);
        }
    }

    //// A
    //public int DivisorSubstrings(int num, int k)
    //{
    //    string s = num.ToString();
    //    int cnt = 0;
    //    for (int i = 0, j = k - 1; j < s.Length; ++i, ++j)
    //    {
    //        int n = int.Parse(s.Substring(i, k));
    //        if (n != 0 && num % n == 0) cnt++;
    //    }
    //    return cnt;
    //}

    //// B
    //public int WaysToSplitArray(int[] nums)
    //{
    //    long sum = 0, all = nums.Select(n => (long)n).Sum();
    //    int cnt = 0;
    //    for (int i = 0; i < nums.Length - 1; ++i)
    //    {
    //        sum += nums[i];
    //        long rest = all - sum;
    //        if (sum >= rest) cnt++;
    //    }
    //    return cnt;
    //}

    ////// problem c
    ////public int MaximumWhiteTiles(int[][] tiles, int carpetLen)
    ////{
    ////    List<(int, int)> list = tiles.OrderBy(t => t[0]).Select(t => (t[0], t[1])).ToList();
    ////    int cover = 0, start = list[0].Item1, end = start + carpetLen;
    ////    int inrange = 0, incover = 0;
    ////    for (; inrange < list.Count; ++inrange)
    ////    {
    ////        if (list[inrange].Item2 >= end) break;
    ////        incover += list[inrange].Item2 - list[inrange].Item1 + 1;
    ////    }

    ////    for (int i = 0; i < list.Count; ++i)
    ////    {
    ////        for 

    ////    }
    ////    foreach ((int left, int right) in tiles.OrderBy(t => t[0]).Select(t => (t[0], t[1])))
    ////    {

    ////    }
    ////}
}
