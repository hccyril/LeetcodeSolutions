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
    public class ProblemC
    {
        public long MinimumTime(int[] time, int totalTrips)
        {
            bool Pass(long t)
            {
                long sum = 0L;
                foreach (int ti in time) sum += t / ti;
                return sum >= totalTrips;
            }
            long l = 1L, r = (long)time.Max() * totalTrips;
            while (l < r)
            {
                long mid = l + r >> 1; // !!
                if (Pass(mid)) r = mid;
                else l = mid + 1;
            }
            return l;
        }

        [TestMethod]
        public void TestMethodC()
        {
            int ans = 10;
            Assert.AreEqual(10, ans);
        }
    }
}
