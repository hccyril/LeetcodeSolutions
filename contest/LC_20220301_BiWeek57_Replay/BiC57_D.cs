using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class BiC57_D
    {
        public int[] CanSeePersonsCount(int[] heights)
        {
            List<int> stk = new();
            int[] ans = new int[heights.Length];
            for (int i = 0; i < heights.Length; i++)
            {
                while (stk.Any() && heights[i] >= heights[stk.Last()])
                {
                    ans[stk.Last()]++;
                    stk.RemoveAt(stk.Count - 1);
                }
                if (stk.Any()) ans[stk.Last()]++;
                stk.Add(i);
            }
            return ans;
        }

        [TestMethod]
        public void Run()
        {
            int ans = 0;
            Assert.AreEqual(1, ans);
        }
    }

 
}
