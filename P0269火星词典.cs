using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
    现有一种使用英语字母的外星文语言，这门语言的字母顺序与英语顺序不同。

    给定一个字符串列表 words ，作为这门语言的词典，words 中的字符串已经 按这门新语言的字母顺序进行了排序 。

    请你根据该词典还原出此语言中已知的字母顺序，并 按字母递增顺序 排列。若不存在合法字母顺序，返回 "" 。若存在多种可能的合法字母顺序，返回其中 任意一种 顺序即可。

    字符串 s 字典顺序小于 字符串 t 有两种情况：

    在第一个不同字母处，如果 s 中的字母在这门外星语言的字母顺序中位于 t 中字母之前，那么 s 的字典顺序小于 t 。
    如果前面 min(s.length, t.length) 字母都相同，那么 s.length < t.length 时，s 的字典顺序也小于 t 。
 

    示例 1：

    输入：words = ["wrt","wrf","er","ett","rftt"]
    输出："wertf"
    示例 2：

    输入：words = ["z","x"]
    输出："zx"
    示例 3：

    输入：words = ["z","x","z"]
    输出：""
    解释：不存在合法字母顺序，因此返回 "" 。
 

    提示：

    1 <= words.length <= 100
    1 <= words[i].length <= 100
    words[i] 仅由小写英文字母组成

    来源：力扣（LeetCode）
    链接：https://leetcode-cn.com/problems/Jf1JuT
    著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

     * */

    // hard, plus, 2022/2/9
    // also 《剑指 Offer II 114. 外星文字典》
    // Floyd算法
    internal class P0269火星词典
    {
        int[,] cmpt = new int[26, 26];

        public bool Check(int i, int j, int k)
        {
            if (k != i && k != j && cmpt[i, k] != 0 && cmpt[i, k] == cmpt[k, j])
            {
                if (cmpt[i, j] != 0 && cmpt[i, j] != cmpt[i, k] ||
                    cmpt[j, i] != 0 && cmpt[j, i] != -cmpt[i, k])
                    return false;
                if (cmpt[i, j] == 0)
                {
                    cmpt[i, j] = cmpt[i, k];
                    cmpt[j, i] = -cmpt[i, k];
                }
            }
            return true;
        }
        public bool Cmp(string w1, string w2)
        {
            int len = Math.Min(w1.Length, w2.Length);
            for (int i = 0; i < len; ++i)
                if (w1[i] != w2[i])
                    return Cmp(w1[i], w2[i]);
            return w1.Length <= w2.Length;
        }
        bool Cmp(char c1, char c2)
        {
            if (c1 == c2) return false;
            int i1 = c1 - 'a', i2 = c2 - 'a';
            if (cmpt[i1, i2] > 0 || cmpt[i2, i1] < 0) return false;
            cmpt[i1, i2] = -1; cmpt[i2, i1] = 1;
            return true;
        }

        int Compare(int x, int y) => cmpt[x, y];

        public string AlienOrder(string[] words)
        {
            BitArray ba = new(26);
            foreach (var w in words)
                foreach (var c in w)
                    ba[c - 'a'] = true;

            for (int i = 0; i < words.Length - 1; ++i)
                for (int j = i + 1; j < words.Length; ++j)
                    if (!Cmp(words[i], words[j]))
                        return "";

            for (int k = 0; k < 26; ++k)
                for (int i = 0; i < 26; ++i)
                    for (int j = 0; j < 26; ++j)
                        if (!Check(i, j, k))
                            return "";

            // 改成用插入排序就没问题了
            List<int> sorted = new();
            foreach (int n in Enumerable.Range(0, 26).Where(i => ba[i]))
            {
                bool added = false;
                for (int i = 0; i < sorted.Count; ++i)
                    if (Compare(n, sorted[i]) < 0)
                    {
                        sorted.Insert(i, n);
                        added = true;
                        break;
                    }
                if (!added) sorted.Add(n);

            }

            return string.Join("", sorted.Select(a => (char)(a + 'a')));
            // 用内置的sort方法会出错！
            // 测试用例：["vlxpwiqbsg","cpwqwqcd"]，自己的输出：bcd...v 但正确答案c应该在v后面
            //Array.Sort(arr, ccmp);
            //return string.Join("", arr.Select(a => (char)(a + 'a')));
        }
    }
}
