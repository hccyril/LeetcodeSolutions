using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium(???), 数学
    internal class P0372超级次方
    {
        public int SuperPow(int a, int[] b)
        {
            const int c = 1337;
            const int phi = 1140;  // phi(1337) = 1140 according to euler
            int n = b.Length;
            if (n == 0) 
                return 1;
            
            // (a^b)%c == (a^(b%phi(c))) % c
            // where c is a prime number and phi(c) is an euler function: https://zh.wikipedia.org/wiki/%E6%AC%A7%E6%8B%89%E5%87%BD%E6%95%B0
        
            // calc b%phi(c) 
            long exp = 0;
            foreach (var digit in b)
                exp = (10 * exp + digit) % phi;
            
            return (int)QuickPow(a, exp, c);
        }
        long QuickPow(long a, long b, long c)
        {
            long res = 1;
            while (b > 0) {
                if ((b & 1) == 1) 
                    res = (res * a) % c;
                b >>= 1;
                a = (a * a) % c;
            }
            return res;
        }
        /* 评论区题解（数学）（GO语言）
         * 0ms，高精度的快速幂就不说了，说下数学优化： 
         * 根据欧拉-费马降幂，a^b %c == a^(b%phi(c)) % c（c是素数） phi(c)是欧拉函数，
         * 表示小于c的和c互质的数的个数。这道题给定c是1137，
         * 所以可以提前求出phi(c) = 1140。你也可以直接求（自己想想怎么实现）。 
         * 求出phi(c)之后，剩下的就很简单啦
        pub fn super_pow(a: i32, b: Vec<i32>) -> i32 {
            const c: i32 = 1337;
            const phi: i32 = 1140;  // phi(1337) = 1140 according to euler
            let n = b.len();
            if n == 0 {
                return 1;
            }
            // (a^b)%c == (a^(b%phi(c))) % c
            // where c is a prime number and phi(c) is an euler function: https://zh.wikipedia.org/wiki/%E6%AC%A7%E6%8B%89%E5%87%BD%E6%95%B0
        
            // calc b%phi(c) 
            let mut exp = 0;
            for digit in b {
                exp = (10*exp+digit) % phi;
            }
            Self::quick_pow(a as i64,exp as i64,c as i64) as i32
        }
        fn quick_pow(mut a: i64, mut b: i64, c: i64) -> i64 {
            let mut res = 1;
            while b > 0 {
                if b & 1 == 1 {
                    res = (res*a)%c;
                }
                b >>=1;
                a = (a*a)%c;
            }
            res
        }
        */
    }
}
