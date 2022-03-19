using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC282_D
    {
        class TireStruct
        {
            public int f;
            public int r;
            public int p;
            public int t;
        }
        public int MinimumFinishTime(int[][] tires, int changeTime, int numLaps)
        {
            int max = tires.Select(t => t[0]).Min() * numLaps + changeTime * (numLaps - 1);
            int[] arr = new int[numLaps + 1];
            var tlist = new List<TireStruct>();
            foreach (var ti in tires.OrderBy(ti => ti[0]))
            {
                if (!tlist.Any() || tlist.Last().r > ti[1])
                    tlist.Add(new TireStruct
                    {
                        f = ti[0],
                        r = ti[1],
                        p = ti[0],
                        t = ti[0]
                    });
            }
            int ptr = 0;
            for (int i = 1; i <= numLaps; ++i)
            {
                arr[i] = tlist[ptr].t;
                for (int j = ptr; j < tlist.Count; ++j)
                {
                    tlist[j].p *= tlist[j].r;
                    tlist[j].t += tlist[j].p;
                    if (j > ptr && tlist[j].t < tlist[ptr].t)
                    {
                        ptr = j;
                    }
                }
                if (tlist[ptr].t >= max) break;
            }
            int[] dp = new int[numLaps + 1];
            for (int lap = 1; lap <= numLaps; lap++)
            {
                dp[lap] = arr[lap] <= 0 ? max : arr[lap];
                int half = lap >> 1;
                for (int k = 1; k <= half; ++k)
                {
                    int test = dp[k] + changeTime + dp[lap - k];
                    if (test < dp[lap]) dp[lap] = test;
                }
            }
            return dp[numLaps];
        }

        [TestMethod]
        public void Run()
        {
            int[] a1 = { 2, 3 }, a2 = { 3, 4 };
            int[][] arr = { a1, a2 };
            int change = 5;
            int lap = 4;
            int ans = MinimumFinishTime(arr, change, lap);

            Assert.AreEqual(21, ans);
        }
    }

 
}
