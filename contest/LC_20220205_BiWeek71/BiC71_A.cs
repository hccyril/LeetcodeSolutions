using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class BiC71_A
    {
        public int MinimumSum(int num)
        {
            int[] arr = { num / 1000, num % 1000 / 100, num % 100 / 10, num % 10 };
            Array.Sort(arr);
            return arr[0] * 10 + arr[1] * 10 + arr[2] + arr[3];
        }


        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
