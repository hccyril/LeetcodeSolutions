using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class BiC57_A
    {
        public bool AreOccurrencesEqual(string s)
        {
            return s.GroupBy(c => c).Select(g => g.Count()).GroupBy(n => n).Count() == 1;
        }

        [TestMethod]
        public void TestMethodA()
        {
            int ans = 1; 
            Assert.AreEqual(2, ans);
        }
    }
}
