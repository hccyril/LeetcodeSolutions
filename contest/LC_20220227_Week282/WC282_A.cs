using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC282_A
    {
        public int PrefixCount(string[] words, string pref)
        {
            return words.Count(t => t.StartsWith(pref));
        }

        [TestMethod]
        public void TestMethodA()
        {
            int ans = 1; 
            Assert.AreEqual(2, ans);
        }
    }
}
