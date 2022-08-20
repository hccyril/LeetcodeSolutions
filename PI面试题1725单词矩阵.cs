using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/27
    // 题目没有说明，但实测所有单词均为小写字母
    // 回溯
    internal class PI面试题1725单词矩阵
    {
        public string[] MaxRectangle(string[] words)
        {
            HashSet<string> wordsDic = new HashSet<string>(words);
            List<string>[,] dic = new List<string>[100, 26],
                gps = new List<string>[100, 26];
            bool Match(string s, int i) // s 的每个字母都可以作为别的单词的第i个字母
            {
                foreach (var c in s)
                    if (dic[i, c - 'a'] == null)
                        return false;
                return true;
            }
            foreach (var w in words)
            {
                if (gps[w.Length - 1, w[0] - 'a'] == null) gps[w.Length - 1, w[0] - 'a'] = new();
                gps[w.Length - 1, w[0] - 'a'].Add(w);

                for (int i = 0; i < w.Length; ++i)
                {
                    int j = w[i] - 'a';
                    if (dic[i, j] == null) dic[i, j] = new();
                    dic[i, j].Add(w);
                }
            }
            string[] ans = Array.Empty<string>();
            List<string> rows = new(), cols = new();
            void Rec(int i)
            {
                if (i == 0)
                {
                    foreach (var sr in words)
                        if (Match(sr, 0))
                        {
                            foreach (var sc in words)
                                if (sc[0] == sr[0] && (sc.Length < sr.Length || sc.Length == sr.Length && sc.CompareTo(sr) <= 0)
                                    && Match(sc, 0))
                                {
                                    if (ans.Any() && ans.Length * ans[0].Length >= sc.Length * sr.Length)
                                        continue;
                                    rows.Clear(); rows.Add(sr);
                                    cols.Clear(); cols.Add(sc);
                                    Rec(i + 1);
                                }
                        }
                }
                else if (i == cols[0].Length)
                {
                    if (rows[0].Length > cols[0].Length)
                        for (int j = i; j < rows[0].Length; j++)
                        {
                            string sc = string.Join("", rows.Select(s => s[j]));
                            if (!wordsDic.Contains(sc))
                                return;
                        }
                    ans = rows.ToArray();
                }
                else
                {
                    foreach (var sr in gps[rows[0].Length - 1, cols[0][i] - 'a'] ?? new List<string>())
                        if (Match(sr, i))
                        {
                            string pre = string.Join("", cols.Select(s => s[i]));
                            if (!sr.StartsWith(pre)) continue;
                            rows.Add(sr);
                            pre = string.Join("", rows.Select(s => s[i]));
                            foreach (var sc in gps[cols[0].Length - 1, rows[0][i] - 'a'] ?? new List<string>())
                                if (sc.StartsWith(pre) && Match(sc, i))
                                {
                                    cols.Add(sc);
                                    Rec(i + 1);
                                    cols.RemoveAt(i);
                                }
                            rows.RemoveAt(i);
                        }
                }
            }
            Rec(0);
            return ans;
        }

        internal static void Run()
        {
            string[] input = {"hjhbr", "dixpgflm", "jjzgr", "gb", "ruzih", "zvthz", "rcadj", "agched", "jwvouurr", "hpmyrbq", "rdzfv", "pdffy", "ihsvg", "dihvb", "fhdwixmy", "cpvhj", "x", "aotsh", "qgahgz", "upoij"};
            var sln = new PI面试题1725单词矩阵();
            Console.WriteLine(string.Join("\n", sln.MaxRectangle(input)));
        }
    }
}
