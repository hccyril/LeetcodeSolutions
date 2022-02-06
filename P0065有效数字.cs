using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 有效数字（按顺序）可以分成以下几个部分：

一个 小数 或者 整数
（可选）一个 'e' 或 'E' ，后面跟着一个 整数
小数（按顺序）可以分成以下几个部分：

（可选）一个符号字符（'+' 或 '-'）
下述格式之一：
至少一位数字，后面跟着一个点 '.'
至少一位数字，后面跟着一个点 '.' ，后面再跟着至少一位数字
一个点 '.' ，后面跟着至少一位数字
整数（按顺序）可以分成以下几个部分：

（可选）一个符号字符（'+' 或 '-'）
至少一位数字
部分有效数字列举如下：

["2", "0089", "-0.1", "+3.14", "4.", "-.9", "2e10", "-90E3", "3e+7", "+6e-1", "53.5e93", "-123.456e789"]
部分无效数字列举如下：

["abc", "1a", "1e", "e3", "99e2.5", "--6", "-+3", "95a54e53"]
给你一个字符串 s ，如果 s 是一个 有效数字 ，请返回 true 。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/valid-number
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    // 还没时间做
    class P0065有效数字
    {
        class NumberReader
        {
            int ind = 0;
            string s;
            int len => s.Length;
            bool ans = true;
            public NumberReader(string s) => this.s = s;

            NumberReader ReadDec()
            {
                if (ans)
                {
                    ReadSign();
                    int c1 = ReadDigits();
                    ReadDot();
                    int c2 = ReadDigits();
                    ans = c1 >= 0 && c2 >= 0 && (c1 > 0 || c2 > 0);
                }
                return this;
            }
            NumberReader ReadExp()
            {
                if (ans)
                {
                    if (ind < len)
                    {
                        ReadCharE().ReadInt();
                    }
                }
                return this;
            }
            NumberReader ReadCharE()
            {
                if (ans)
                {
                    char c = s[ind++];
                    ans = c == 'E' || c == 'e';
                }
                return this;
            }
            NumberReader ReadSign()
            {
                if (ans)
                {
                    if (ind < len && (s[ind] == '+' || s[ind] == '-')) ind++;
                }
                return this;
            }
            NumberReader ReadDot()
            {
                if (ans)
                {
                    if (ind < len && s[ind] == '.') ind++;
                }
                return this;
            }
            int ReadDigits()
            {
                if (ans)
                {
                    int nCount = 0;
                    for (; ind < len && s[ind] >= '0' && s[ind] <= '9'; ++nCount, ++ind) ;
                    return nCount;
                }
                return -1;
            }
            NumberReader ReadInt()
            {
                if (ans)
                {
                    ans = ReadSign().ReadDigits() > 0;
                }
                return this;
            }
            bool Answer()
            {
                return ans && ind >= len;
            }
            public bool Run()
            {
                return ReadDec().ReadExp().Answer();
            }
        }
        public bool IsNumber(string s)
        {
            return new NumberReader(s).Run();

            // failed
            //if (s.StartsWith("e") || s.StartsWith("E")) return false;
            //return new System.Text.RegularExpressions.Regex("([+-]?(\\d+(\\.\\d*)?|(\\.\\d+)))([Ee][+-]?\\d+)?").IsMatch(s);
        }
    }
}
