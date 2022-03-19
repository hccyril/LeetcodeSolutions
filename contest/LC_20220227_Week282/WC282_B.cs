using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC282_B
    {
        public int MinSteps(string s, string t)
        {
            int[] arrs = new int[26], arrt = new int[26];
            foreach (var c in s) arrs[c - 'a']++;
            foreach (var c in t) arrt[c - 'a']++;
            int sum = 0;
            for (int i = 0; i < 26; ++i)
            {
                sum += Math.Max(arrs[i], arrt[i]) - Math.Min(arrs[i], arrt[i]);
            }
            return sum;
        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
