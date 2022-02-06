using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 483.《最小好进制》
     * 对于给定的整数 n, 如果n的k（k>=2）进制数的所有数位全为1，则称 k（k>=2）是 n 的一个好进制。

以字符串的形式给出 n, 以字符串的形式返回 n 的最小好进制。

 

示例 1：

输入："13"
输出："3"
解释：13 的 3 进制是 111。
示例 2：

输入："4681"
输出："8"
解释：4681 的 8 进制是 11111。
示例 3：

输入："1000000000000000000"
输出："999999999999999999"
解释：1000000000000000000 的 999999999999999999 进制是 11。
 

提示：

n的取值范围是 [3, 10^18]。
输入总是有效且没有前导 0。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/smallest-good-base
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    // 这题真不会，参考了评论之后做的
    class P0483最小好进制
    {
        public string SmallestGoodBase(string n)
        {
            long num = long.Parse(n);
            int max_m = (int)Math.Log2(num + 1);

            //对进行m的大小进行穷举（m含义是转换为k进制后1的个数）
            for (int m = max_m; m >= 2; --m)
            {
                //用二分法搜索对应的k,(k的含义是k进制)
                long left = 2, right = (long)Math.Pow(num, 1.0 / (m - 1)) + 1;
                while (left < right)
                {
                    long mid = left + (right - left) / 2, sum = 0;

                    //等比数列求和
                    for (int j = 0; j < m; j++)
                        sum = sum * mid + 1;

                    if (sum == num)
                        return mid.ToString();
                    else if (sum < num)
                        left = mid + 1;
                    else
                        right = mid;
                }
            }
            return (num - 1).ToString();
        }
    }
}
