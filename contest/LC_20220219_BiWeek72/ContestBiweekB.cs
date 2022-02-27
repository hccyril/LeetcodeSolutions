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
        public long[] SumOfThree(long num)
        {
            if (num % 3L == 0)
            {
                return new long[] { num / 3L - 1L, num / 3L, num / 3L + 1L };
            }
            return new long[0];
        }


        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
