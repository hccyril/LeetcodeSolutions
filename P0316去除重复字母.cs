using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/3/18 US Daily
    // 相同题：1081，rank: 2185
    internal class P0316去除重复字母
    {
        public string RemoveDuplicateLetters(string s)
        {
            List<int>[] arr = new List<int>[26];
            for (int i = 0; i < 26; i++) arr[i] = new();
            for (int i = 0; i < s.Length; ++i) arr[s[i] - 'a'].Add(i);
            int[] pos = new int[26];
            for (int i = 0; i < 26; i++)
                pos[i] = arr[i].Any() ? -1 : -2;
            int start = -1, count = pos.Count(p => p == -1);
            while (count-- > 0)
            {
                var range = Enumerable.Range(0, 26).Where(t => pos[t] == -1);
                foreach (int i in range)
                {
                    int f = ~arr[i].BinarySearch(start);
                    if (f < arr[i].Count && !range.Any(c => c != i && arr[c].Last() <= arr[i][f]))
                    {
                        pos[i] = start = arr[i][f];
                        break;
                    }
                }
            }
            BitArray ba = new(s.Length);
            foreach (var p in pos) if (p != -2) ba[p] = true;
            return string.Join("", Enumerable.Range(0, s.Length).Where(i => ba[i]).Select(i => s[i]));
        }

        // ver1 - WA for s = "cbacdcbc"
        public string RemoveDuplicateLetters_1(string s)
        {
            Dictionary<char, List<int>> dic = new();
            for (var c = 'a'; c <= 'z'; ++c) dic[c] = new();
            for (int i = 0; i < s.Length; ++i) dic[s[i]].Add(i);
            Dictionary<char, int> ca = new();
            BitArray ba = new(s.Length);
            for (var c = 'a'; c <= 'z'; ++c)
            {
                ca[c] = -1;
                if (dic[c].Any())
                {
                    var list = dic[c];
                    int start = 0;
                    for (var d = 'a'; d < c; ++d)
                    {
                        if (ca[d] >= 0)
                        {
                            int i = ~list.BinarySearch(start, list.Count - start, ca[d], null);
                            if (i < list.Count) start = i;
                        }
                    }
                    ca[c] = list[start];
                }
                if (ca[c] >= 0) ba[ca[c]] = true;
            }
            return string.Join("", Enumerable.Range(0, s.Length).Where(i => ba[i]).Select(i => s[i]));
        }

        internal static void Run()
        {
            var sln = new P0316去除重复字母();
            string s = "cbacdcbc";
            Console.WriteLine(sln.RemoveDuplicateLetters(s));
        }
    }
}
