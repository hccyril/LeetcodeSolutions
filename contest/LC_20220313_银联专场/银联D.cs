using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class 银联D
    {
        public int CoopDevelop(int[][] skills)
        {
            Dictionary<long, long> mp = new();
            long ans = 0;
            int i = 0;
            foreach (var ski in skills.OrderBy(t => t.Length))
            {
                ans += i++;
                long t = 0;
                for (int j = 0; j < (1 << ski.Length); j++)
                {
                    t = 0;
                    for (int k = 0; k < ski.Length; k++)
                    {
                        if (((j >> k) & 1) == 1)
                        {
                            t = t * 1000 + ski[k];
                        }
                    }
                    if (mp.ContainsKey(t)) ans -= mp[t];
                }
                t = 0;
                for (int j = 0; j < ski.Length; j++)
                {
                    t = t * 1000 + ski[j];
                }
                if (!mp.ContainsKey(t)) mp.Add(t, 1L);
                else ++mp[t];
            }
            ans %= 1000000007L;
            return (int)ans;
        }

        [TestMethod]
        public void Run()
        {
            int[] k1 = { 1, 2, 3 }, k2 = { 3 }, k3 = { 2, 4 };
            int[][] skills = { k1, k2, k3 };
            int ans = CoopDevelop(skills);
            Assert.AreEqual(2, ans);
        }
    }
}
