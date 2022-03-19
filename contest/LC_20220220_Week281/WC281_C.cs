using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC281_C
    {
		public string RepeatLimitedString(string s, int repeatLimit) {
			int len = s.Length;
			int[] arr = new int[26];
			foreach (var c in s) ++arr[c - 'a'];
			StringBuilder sb = new();
			int a = -1, rep = repeatLimit;
			while (len > 0) {
				bool ok = false;
				for (int i = 25; i>= 0; --i) {
					if (rep == 0 && i == a || arr[i] == 0) continue;
					sb.Append((char)(i + 'a'));
					--len;
					--arr[i];
					if (i == a) --rep;
					else {
						a = i;
						rep = repeatLimit - 1;
					}
					ok = true; break;
				}
				if (!ok) break;
			}
			return sb.ToString();
		}

        [TestMethod]
        public void TestMethodC()
        {
            int ans = 10;
            Assert.AreEqual(10, ans);
        }
    }
}
