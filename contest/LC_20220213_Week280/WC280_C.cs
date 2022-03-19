using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC280_C
    {
        public long MinimumRemoval(int[] beans)
        {
            Array.Sort(beans);
            long sum = 0; foreach (var b in beans) sum += b;
            long ans = sum, min = 0;
            for (int i = 0; i < beans.Length; ++i)
            {
                if (i > 0) sum += beans[i - 1];
                sum -= (beans[i] - min) * (beans.Length - i);
                min = beans[i];
                if (sum < ans) ans = sum;
            }
            return ans;
        }


        [TestMethod]
        public void TestMethodC()
        {
            int ans = 10;
            Assert.AreEqual(10, ans);
        }
    }
}
