using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* medium
     * 给定一个字符串数组 words，找到 length(word[i]) * length(word[j]) 的最大值，
     * 并且这两个单词不含有公共字母。你可以认为每个单词只包含小写字母。如果不存在这样的两个单词，返回 0。

        链接：https://leetcode-cn.com/problems/maximum-product-of-word-lengths
    */
    class P0318最大单词长度乘积
    {
        class WordStruct
        {
            public int len, map = 0;
            public WordStruct(string word)
            {
                len = word.Length;
                foreach (char c in word)
                    map |= 1 << (c - 'a');
            }
        }
        public int MaxProduct(string[] words)
        {
            int max = -1;
            WordStruct[] arr = words
                .Select(t => new WordStruct(t))
                .OrderBy(t => t.len) // 第一次犯了低级错误，这里用了OrderByDescending，后面数组又是倒序遍历
                .ToArray();
            int prod = 0;
            for (int i = arr.Length - 1; i >= 0; --i)
                for (int j = i - 1; j >= 0 && (prod = arr[i].len * arr[j].len) > max; --j)
                    if ((arr[i].map & arr[j].map) == 0)
                        max = prod;
            return max;
        }
    }
}
