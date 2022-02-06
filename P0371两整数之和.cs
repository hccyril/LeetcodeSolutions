using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{

    // 位运算基础 (bit operations)
    /// <summary>
    /// 位运算方法求两数相加的结果
    /// </summary>
    class P0371两整数之和
    {
        public int GetSum(int a, int b) => b == 0 ? a : GetSum(a ^ b, (a & b) << 1);

        // 加法：a^b
        // 进位：(a&b) << 1

        //public int GetSum(int a, int b)
        //{
        //    while (b != 0)
        //    {
        //        int carry = (a & b) << 1;
        //        a = a ^ b;
        //        b = carry;
        //    }
        //    return a;
        //    //return a + b;
        //}
    }
}
