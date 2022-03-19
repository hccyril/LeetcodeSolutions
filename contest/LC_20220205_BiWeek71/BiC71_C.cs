using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class BiC71_C
    {
        public int MinCostSetTime(int startAt, int moveCost, int pushCost, int targetSeconds)
        {
            int min = targetSeconds / 60, sec = targetSeconds % 60;
            int cost = CalcCost(startAt, moveCost, pushCost, min, sec);
            while (min > 0)
            {
                --min; sec += 60;
                if (sec > 99) break;
                cost = Math.Min(cost, CalcCost(startAt, moveCost, pushCost, min, sec));
            }
            return cost;
        }

        private int CalcCost(int startAt, int moveCost, int pushCost, int min, int sec)
        {
            if (min > 99 || sec > 99) return int.MaxValue;
            string s = min > 0 ? min.ToString() + sec.ToString("D2") : sec.ToString();
            int cost = 0, num = startAt;
            foreach (var c in s)
            {
                int n = c - '0';
                if (n != num) cost += moveCost;
                num = n;
                cost += pushCost;
            }
            return cost;
        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
