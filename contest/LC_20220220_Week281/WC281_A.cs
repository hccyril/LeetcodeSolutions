using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC281_A
    {
		public int CountEven(int num) {
			int ans = 0;
			for (int i = 1; i <= num; ++i)
			{
				int sum = i.ToString().Select(c => c - '0').Sum();
				if ((sum & 1) == 0) ans++;
			}
			return ans;
		}
		
        int test(int num)
        {
            int ans = 0;
            for (int i = 0; i < num; ++i)
            {
                int sum = i.ToString().Select(c => c - '0').Sum();
                if ((sum & 1) == 0) ans++;
            }
            return ans;
        }

        [TestMethod]
        public void TestMethodA()
        {
            int ans = 1; 
            Assert.AreEqual(2, ans);
        }
    }
}
