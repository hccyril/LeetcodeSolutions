using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCore1
{
    // hard, 2021/6/14, 差一个用例超时最后写死过了
    // 2022/6/23 Daily, Rework - 才发现没审题，words的所有单词长度都是相同的
    // 2022/7/14 再次重写终于通过

    /*
     * 给定一个字符串 s 和一些 长度相同 的单词 words 。找出 s 中恰好可以由 words 中所有单词串联形成的子串的起始位置。

    注意子串要与 words 中的单词完全匹配，中间不能有其他字符 ，但不需要考虑 words 中单词串联的顺序。

    示例 1：
    输入：s = "barfoothefoobarman", words = ["foo","bar"]
    输出：[0,9]
    解释：
    从索引 0 和 9 开始的子串分别是 "barfoo" 和 "foobar" 。
    输出的顺序不重要, [9,0] 也是有效答案。

    示例 2：
    输入：s = "wordgoodgoodgoodbestword", words = ["word","good","best","word"]
    输出：[]

    示例 3：
    输入：s = "barfoofoobarthefoobarman", words = ["bar","foo","the"]
    输出：[6,9,12]
 
    提示：
    1 <= s.length <= 10^4
    s 由小写英文字母组成
    1 <= words.length <= 5000
    1 <= words[i].length <= 30
    words[i] 由小写英文字母组成

    来源：力扣（LeetCode）
    链接：https://leetcode-cn.com/problems/substring-with-concatenation-of-all-words
    著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0030串联所有单词的子串
    {
        // 2022/7/14 双指针 2250ms -> 15ms
        public IList<int> FindSubstring(string s, string[] words)
        {
            List<int> ans = new();
            Dictionary<string, int> dic = new();
            foreach (string word in words)
            {
                dic.TryAdd(word, 0);
                ++dic[word];
            }
            for (int start = 0; start < words[0].Length; ++start)
            {
                for (int i = start, j = start; j + words[0].Length <= s.Length; )
                {
                    string sj = s.Substring(j, words[0].Length);
                    dic.TryAdd(sj, 0);
                    --dic[sj];
                    while (dic[sj] < 0)
                    {
                        string si = s.Substring(i, words[0].Length);
                        ++dic[si];
                        i += words[0].Length;
                    }
                    if ((j += words[0].Length) - i == words.Length * words[0].Length)
                    {
                        ans.Add(i);
                    }
                    // 漏了必须的收尾工作
                    if (j + words[0].Length > s.Length)
                        while (i < j)
                        {
                            string si = s.Substring(i, words[0].Length);
                            ++dic[si];
                            i += words[0].Length;
                        }
                }
            }
            return ans;
        }

        /*
         * 都做到这样了还是超时T_T|||, 最后那个用例用时2秒多
         */

        Dictionary<int, IList<int>> _dic = new Dictionary<int, IList<int>>();

        bool Rec(int i, HashSet<int> hs, string[] words)
        {
            if (hs.Count == words.Length) return true;

            if (_dic.ContainsKey(i))
            {
                string checkedWords = "";
                foreach (var wi in _dic[i])
                {
                    var word = words[wi];
                    if (word != checkedWords && !hs.Contains(wi))
                    {
                        hs.Add(wi);
                        checkedWords = word;
                        if (Rec(i + word.Length, hs, words))
                            return true;
                        hs.Remove(wi);
                    }
                }
            }

            return false;
        }

        public IList<int> FindSubstring_Ver1_TLE(string s, string[] words)
        {
            var listWords = new List<string>(words); listWords.Sort();
            words = listWords.ToArray();
            for (int i = 0; i < words.Length; ++i)
            {
                var word = words[i];
                int j = i + 1; while (j < words.Length && words[j] == word) ++j; --j;

                int index = s.IndexOf(word);
                while (index >= 0)
                {
                    if (!_dic.ContainsKey(index))
                    {
                        _dic.Add(index, new List<int>());
                    }
                    for (int k = i; k <= j; ++k)
                        _dic[index].Add(k);
                    index = s.IndexOf(word, index + 1);
                }

                i = j;
            }
            int totalLength = words.Sum(t => t.Length);
            HashSet<int> hs = new HashSet<int>();
            IList<int> retList = new List<int>();
            for (int i = 0; i <= s.Length - totalLength; ++i)
            {
                if (_dic.ContainsKey(i) && Rec(i, hs, words))
                {
                    retList.Add(i);
                    hs.Clear();
                }
            }

            return retList;
        }

        internal static void Run()
        {
            var sln = new P0030串联所有单词的子串();
            StringBuilder build = new();
            for (int i = 0; i < 5000; ++i)
                build.Append("ab");
            string s = build.ToString();
            List<string> list = new();
            for (int i = 0; i < 100; ++i)
            {
                list.Add("ab");
                list.Add("ba");
            }

            //var ans = sln.FindSubstring_Ver1_TLE(s, list.ToArray());// 2250ms
            var ans = sln.FindSubstring("aaaaaaaaaaaaaa", new string[] { "aa", "aa" }); // 15ms
            Console.WriteLine(nameof(P0030串联所有单词的子串) + $": [{string.Join(",", ans)}]");
        }
    }
}
