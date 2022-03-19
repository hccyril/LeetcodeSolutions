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
        public int MostFrequent(int[] nums, int key)
        {
            List<int> list = new();
            for (int i = 1; i < nums.Length; ++i)
                if (nums[i - 1] == key) list.Add(nums[i]);
            return list.GroupBy(n => n).OrderByDescending(gp => gp.Count()).Select(gp => gp.Key).First();
        }


        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
