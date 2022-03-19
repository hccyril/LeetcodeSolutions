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
    public class ProblemA
    {
        public IList<int> FindKDistantIndices(int[] nums, int key, int k)
        {
            HashSet<int> hs = new();
            for (int i = 0; i < nums.Length; ++i)
            {
                if (nums[i] == key)
                {
                    int min = Math.Max(0, i - k);
                    int max = Math.Min(nums.Length - 1, i + k);
                    for (int j = min; j <= max; ++j)
                        hs.Add(j);
                }
            }
            return Enumerable.Range(0, nums.Length).Where(i => hs.Contains(i)).ToList();
        }

        [TestMethod]
        public void TestMethodA()
        {
            int ans = 1; 
            Assert.AreEqual(2, ans);
        }
    }
}
