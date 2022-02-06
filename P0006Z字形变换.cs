using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 将一个给定字符串 s 根据给定的行数 numRows ，以从上往下、从左到右进行 Z 字形排列。

比如输入字符串为 "PAYPALISHIRING" 行数为 3 时，排列如下：

P   A   H   N
A P L S I I G
Y   I   R
之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："PAHNAPLSIIGYIR"。

请你实现这个将字符串进行指定行数变换的函数：

string convert(string s, int numRows);

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/zigzag-conversion
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0006Z字形变换
    {
        public void MakeStr(StringBuilder sb, string s, int start, int inc1, int inc2)
        {
            int ind = start;
            bool add1 = true;
            while (ind < s.Length)
            {
                sb.Append(s[ind]);
                ind += add1 ? inc1 : inc2;
                add1 = !add1;
            }
        }

        public string Convert(string s, int numRows)
        {
            int start, inc1, inc2;
            StringBuilder sb = new StringBuilder();
            for (int line = 0; line < numRows; ++line)
            {
                start = line;
                if (line == 0 || line == numRows - 1)
                {
                    inc1 = inc2 = numRows < 2 ? 1 : 2 * (numRows - 1);
                }
                else
                {
                    inc1 = (numRows - line - 1) * 2;
                    inc2 = line * 2;
                }

                MakeStr(sb, s, start, inc1, inc2);
            }
            return sb.ToString();
        }
    }
}
