using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    // 比赛时题目描述写错，导致提交5次才过

    [TestClass]
    public class ProblemC
    {
        public int MaximumTop(int[] nums, int k)
        {
            if (nums.Length == 1)
            {
                //return k == 0 ? nums[0] : -1;
                return (k & 1) == 1 ? -1 : nums[0];
            }
            else if (nums.Length == 2)
            {
                if (k == 0) return nums[0];
                else if (k == 1) return nums[1];
                else if (k == 2) return nums[0];
                //else return (k & 1) == 0 ? -1 : nums.Max();
                return nums.Max();
            }
            else
            {
                if (k == 0) return nums[0];
                if (k == 1) return nums[1];
                //if (k == 2) return nums[2];
                if (k > nums.Length) return nums.Max();
                if (k == nums.Length) return nums.Take(k - 1).Max();
                return Math.Max(nums.Take(k - 1).Max(), nums[k]);
            }
        }

        [TestMethod]
        public void TestMethodC()
        {
            int[] input = { 31, 15, 92, 84, 19, 92, 55 };
            int output = MaximumTop(input, 4);
            //var input = new int[][] { new int[] { 1, 4, 5 }, new int[] { 1, 4, 7 }, new int[] { 4, 7, 1 }, new int[] { 4, 7, 11 } };
            //var output = SplitPainting(input);
            Assert.AreEqual(92, output);
        }
    }
}
