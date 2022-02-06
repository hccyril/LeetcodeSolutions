using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     给你一个 32 位的有符号整数 x ，返回将 x 中的数字部分反转后的结果。

如果反转后整数超过 32 位的有符号整数的范围 [−231,  231 − 1] ，就返回 0。

假设环境不允许存储 64 位整数（有符号或无符号）。
 

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/reverse-integer
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

     * */

    class P0007整数反转
    {
        const string MAX_S = "2147483647";
        const string MIN_S = "-2147483648";

        bool Exceed(string s, int x)
        {
            //if (x > 0 && s.CompareTo(MAX_S) > 0 || x < 0 && s.CompareTo(MIN_S) > 0)
            if (x > 0)
            {
                if (s.Length > 10 || s.Length == 10 && s.CompareTo(MAX_S) > 0)
                    return true;
            }
            else
            {
                if (s.Length > 11 || s.Length == 11 && s.CompareTo(MIN_S) > 0)
                    return true;
            }
            return false;
        }
        public int Reverse(int x)
        {
            if (x == 0) return 0;
            string s = x.ToString();
            if (x < 0) s = s.TrimStart('-');
            var t = s.Reverse();
            s = new string(t.ToArray());
            s = s.TrimStart('0');
            if (x < 0) s = "-" + s;
            if (Exceed(s, x)) return 0;
            return int.Parse(s);
        }
    }
}
