using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC280_A
    {
        public int CountOperations(int num1, int num2)
        {
            return num1 == 0 || num2 == 0 ? 0 : 1 + CountOperations(Math.Min(num1, num2), Math.Max(num1, num2) - Math.Min(num1, num2));
        }

        [TestMethod]
        public void TestMethodA()
        {
            int ans = 1; 
            Assert.AreEqual(2, ans);
        }
    }
}
