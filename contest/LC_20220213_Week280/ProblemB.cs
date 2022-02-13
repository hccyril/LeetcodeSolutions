using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ProblemB
    {
        public int MinimumOperations(int[] nums)
        {
            if (nums.Length <= 1) return 0;
            else if (nums.Length == 2) return nums[0] == nums[1] ? 1 : 0;

            // quick test
            int n = nums[0];
            bool test = true;
            for (int i = 1; i < nums.Length; ++i) if (nums[i] != n) { test = false; break; }
            if (test) return nums.Length >> 1;

            // start
            HashHeap hp0 = new(true), hp1 = new(true);
            int flip = 0;
            for (int i = 0; i < nums.Length; ++i)
            {
                if (flip == 0)
                {
                    n = nums[i];
                    if (hp0.ContainsKey(n)) hp0.Update(n, 1);
                    else hp0.Push(n, 1);
                }
                else
                {
                    n = nums[i];
                    if (hp1.ContainsKey(n)) hp1.Update(n, 1);
                    else hp1.Push(n, 1);
                }
                flip ^= 1;
            }

            n = nums.Length;
            (int k0, int n0) = hp0.Pop();
            (int k1, int n1) = hp1.Pop();
            if (k0 != k1) return n / 2 - n1 + (n - n / 2) - n0;
            int ans = int.MaxValue;
            if (hp0.Any())
            {
                (int k2, int n2) = hp0.Pop();
                ans = n / 2 - n1 + (n - n / 2) - n2;
            }
            if (hp1.Any())
            {
                (int k2, int n2) = hp1.Pop();
                ans = Math.Min(ans, n / 2 - n2 + (n - n / 2) - n0);
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
