using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 1239. 串联字符串的最大长度
给定一个字符串数组 arr，字符串 s 是将 arr 某一子序列字符串连接所得的字符串，如果 s 中的每一个字符都只出现过一次，那么它就是一个可行解。

请返回所有可行解 s 中最长长度。
    https://leetcode-cn.com/problems/maximum-length-of-a-concatenated-string-with-unique-characters/

     * */
    class P1239串联字符串的最大长度
    {
        HashSet<int> _hs = new HashSet<int>();

        int GetBitLen(int n)
        {
            return n == 0 ? 0 : GetBitLen(n & (n - 1)) + 1;
        }

        void Add(string s)
        {
            // 将s转为bits表
            int bits = 0;
            foreach (var c in s)
            {
                var bt = 1 << (c - 97);
                if ((bits & bt) == 0)
                {
                    bits |= bt;
                }
                else
                    return; // s 本身包含重复字符
            }

            // 添加
            foreach (var bt0 in new List<int>(_hs))
            {
                if ((bt0 & bits) == 0)
                {
                    int new_bt = bt0 | bits;
                    if (!_hs.Contains(new_bt))
                    {
                        _hs.Add(new_bt);
                    }
                }
            }
            if (!_hs.Contains(bits))
            {
                _hs.Add(bits);
            }
        }

        public int MaxLength(IList<string> arr)
        {
            foreach (var s in arr)
            {
                Add(s);
            }
            return _hs.Any() ? _hs.Max(t => GetBitLen(t)) : 0;
        }
    }
}
