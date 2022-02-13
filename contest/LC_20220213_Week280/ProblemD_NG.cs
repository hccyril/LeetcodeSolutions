using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ProblemD
    {
        int maxAns = 0;
        int Dfs(int[] cnts, int[] nums, int i)
        {
            if (i < 0) // TODO
            {

            }
            if (CanFit(cnts, nums, i))
            {
                
            }
        }

        private bool CanFit(int[] cnts, int[] nums, int i)
        {
            if (i >= 8) return cnts[8] > 0 || cnts[9] > 0;
            else if (i >= 4) return cnts[7] > 0 || cnts[6] > 0 || cnts[5] > 0 || cnts[4] > 0;
            else if (i >= 2) return cnts[3] > 0 || cnts[2] > 0;
            else return cnts[1] > 0;
        }

        public int MaximumANDSum(int[] nums, int numSlots)
        {
            int[] cnts = new int[9];
            for (int i = 1; i <= numSlots; ++i) cnts[i] = 2;
            Array.Sort(nums);
            Array.Reverse(nums);


        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }

 
}
