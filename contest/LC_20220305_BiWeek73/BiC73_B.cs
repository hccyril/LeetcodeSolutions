using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ContestBiweekB
    {
        int[] mapping;
        int Map(int n)
        {
            if (n == 0) return mapping[0];
            int mul = 1, a = 0;
            while (n > 0)
            {
                a += mapping[n % 10] * mul;
                n /= 10;
                if (n > 0) mul *= 10;
            }
            return a;
        }
        public int[] SortJumbled(int[] mapping, int[] nums)
        {
            this.mapping = mapping;
            return nums.Select(n => (n, Map(n))).OrderBy(p => p.Item2).Select(p => p.Item1).ToArray();
        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
