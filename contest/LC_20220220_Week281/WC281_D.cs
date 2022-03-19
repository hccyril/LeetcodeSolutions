using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC281_D
    {
        // 比赛结束了10分钟才做对
        public long CoutPairs(int[] nums, int k)
        {
            List<int> prm = new(), arr = new();
            int kk = k;
            for (int p = 2; p * p <= kk; ++p)
            {
                if (k % p == 0)
                {
                    k /= p;
                    prm.Add(p);
                    int pn = 1;
                    while (k % p == 0)
                    {
                        pn++;
                        k /= p;
                    }
                    arr.Add(pn);
                }
            }
            if (prm.Count == 0)
            {
                long n1 = 0, nk = 0;
                foreach (var n in nums)
                    if (n % k == 0) nk++; else n1++;
                return n1 * nk + nk * (nk - 1) / 2; // TODO
            }
            if (k > 1)
            {
                prm.Add(k);
                arr.Add(1);
            }
            Dictionary<int, long> dic = new();
            long ans = 0;
            for (int indx = 0; indx < nums.Length; ++indx)
            {
                int n = nums[indx];
                int f = 1, pf = 1;
                for (int i = 0; i < prm.Count /*&& prm[i] * prm[i] <= n*/; ++i)
                {
                    int p = prm[i], pn = arr[i];
                    while (n % p == 0 && pn > 0)
                    {
                        n /= p;
                        f *= p;
                        pn--;
                    }
                    while (pn-- > 0) pf *= p; 
                }
                foreach (var key in dic.Keys)
                {
                    if (key % pf == 0) ans += dic[key];
                }
                if (!dic.ContainsKey(f)) dic[f] = 0;
                ++dic[f];
            }
            return ans;
        }

        [TestMethod]
        public void Run()
        {
            int[] arr = { 2,5,9,6 };
            int k = 6;
            long ans = CoutPairs(arr, k);
            //int i = 1;
            Assert.AreEqual(4L, ans);
        }
    }

 
}
