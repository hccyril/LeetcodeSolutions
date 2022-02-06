using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 位运算
    // 据说是阿里笔试第一题
    class P1318或运算的最小翻转次数
    {
        int Count(int n) => n == 0 ? 0 : 1 + Count(n & (n - 1));
        public int MinFlips(int a, int b, int c)
        {
            int cDiff = c ^ (a | b);
            return Count(a & b & cDiff) + Count(cDiff);
        }
    }
    /*
     * 我们只看一位的情况：设 ca = a OR b，即目前实际运算结果，然后与 c 作对比，当 ca 和 c 不一样时，有下列情况：

a 跟 b 都是 1，ca=1，所以 c 肯定是 0（因为前提是 c 跟 ca 不一样），此时我们只能将 a 和 b 都给翻成 0，需要翻2次
a 跟 b 有一个是 1，ca=1，所以 c=0，我们将 a b 里 1 那个翻成 0 就可以了，需要翻 1 次
a 跟 b 都是 0，ca=0，所以 c=1，只需要将 a b 任意一个翻成 1 就可以，需要翻 1 次
进一步总结，其实就只需要看 ca 和 c 不一样的那几位，每一位至少翻 1 次，然后对于情形 1 (可以写成 a & b ==1）需要多翻一次

最后充分利用位运算，代码就非常简单了
    */
}
