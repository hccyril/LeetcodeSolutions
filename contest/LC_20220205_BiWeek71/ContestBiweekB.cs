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
        public int[] PivotArray(int[] nums, int pivot)
        {
            List<int> left = new(), right = new();
            int count = 0;
            foreach (var n in nums)
            {
                if (n < pivot) left.Add(n);
                else if (n > pivot) right.Add(n);
                else count++;
            }
            int i = 0;
            foreach (var l in left) nums[i++] = l;
            while (count-- > 0) nums[i++] = pivot;
            foreach (var r in right) nums[i++] = r;
            return nums;
        }


        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
