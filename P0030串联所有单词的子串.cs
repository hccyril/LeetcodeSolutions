using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
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

1 <= s.length <= 104
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

        public IList<int> FindSubstring(string s, string[] words)
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

       
    }
}
