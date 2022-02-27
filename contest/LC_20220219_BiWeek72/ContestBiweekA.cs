using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ContestBiweekA
    {
        public int CountPairs(int[] nums, int k)
        {
            int ans = 0;
            for (int i = 0; i < nums.Length - 1; ++i)
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[i] == nums[j] && i * j % k == 0)
                        ++ans;
                }
            return ans;
        }


        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
