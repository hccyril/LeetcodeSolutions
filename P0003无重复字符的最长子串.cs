using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium
    // 相同题目：《剑指 Offer II 016. 不含重复字符的最长子字符串》https://leetcode-cn.com/problems/wtcaE1/
    // ver1 2021/6/2
    // ver2 2022/2/3 滑动窗口（比ver1快一倍左右）
    internal class P0003无重复字符的最长子串
    {
        public int LengthOfLongestSubstring_ver2(string s)
        {
            int maxlen = 0;
            BitArray ba = new(256);
            for (int i = 0, j = 0; j < s.Length; ++j)
            {
                int k = s[j];
                while (ba[k]) ba[s[i++]] = false;
                ba[k] = true;
                maxlen = Math.Max(maxlen, j - i + 1);
            }
            return maxlen;
        }

        public int LengthOfLongestSubstring_ver1(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            int max = 1;
            List<List<char>> list = new List<List<char>>();
            foreach (char c in s)
            {
                for (int i = list.Count - 1; i >= 0; --i)
                {
                    if (list[i].Contains(c))
                    {
                        list.RemoveAt(i);
                    }
                    else
                    {
                        list[i].Add(c);
                        if (list[i].Count > max) max = list[i].Count;
                    }
                }
                List<char> l = new List<char>();
                l.Add(c);
                list.Add(l);
            }
            return max;
        }
    }
}
