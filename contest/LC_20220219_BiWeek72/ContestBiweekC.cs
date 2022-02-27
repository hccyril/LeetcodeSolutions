using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ContestBiweekC
    {
        public IList<long> MaximumEvenSplit(long finalSum)
        {
            if (finalSum % 2L != 0) return new List<long>();
            IList<long> list = new List<long>();
            long n = 2L;
            while (finalSum - n > n)
            {
                list.Add(n);
                finalSum -= n;
                n += 2L;
            }
            if (finalSum > 0) list.Add(finalSum);
            return list;
        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
