using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCore1;

namespace ProblemTests
{

    //public static class SolutionExtensions
    //{
    //    const int MOD = 1000000007;
    //    /// <summary>
    //    /// add and MOD 10^9+7
    //    /// </summary>
    //    public static int Add(this int x, int y)
    //    {
    //        int sum = x + y;
    //        if (sum >= MOD) sum -= MOD;
    //        return sum;
    //    }
    //}

    [TestClass]
    public class 银联C
    {
        bool Check(int a, int b, int li)
        {
            return li / b >= a;
        }
        int Sum(int a, int b, int n)
        {
            if (n % 2 == 0)
            {
                return (n / 2).Multi(a.Add(b));
            }
            else
            {
                return ((a + b) / 2).Multi(n);
            }
        }
        public int MaxInvestment(int[] product, int limit)
        {
            Array.Sort(product);
            Array.Reverse(product);
            int ans = 0;
            for (int i = 0; i < product.Length && limit > 0; ++i)
            {
                int next = i == product.Length - 1 ? 0 : product[i + 1];
                if (Check(product[i] - next, i + 1, limit))
                {
                    ans = ans.Add(Sum(next + 1, product[i], product[i] - next).Multi(i + 1));
                    limit -= (product[i] - next) * (i + 1);
                }
                else
                {
                    for (int inv = product[i]; inv >= next; --inv)
                    {
                        if (limit > i + 1)
                        {
                            limit -= i + 1;
                            ans = ans.Add(inv.Multi(i + 1));
                        }
                        else
                        {
                            ans = ans.Add(inv.Multi(limit));
                            return ans;
                        }
                    }
                }
            }
            return ans;

        }

        [TestMethod]
        public void Run()
        {
            /*[4,5,3]
8
            26*/
            int[] input = { 4, 5, 3 };
            int ans = MaxInvestment(input, 8);
            Assert.AreEqual(26, ans);
        }
    }
}
