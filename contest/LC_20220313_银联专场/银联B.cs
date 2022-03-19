using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    public class DiscountSystem
    {
        class Activity
        {
            public int actId; public int priceLimit; public int discount; public int number; public int userLimit;
        }
        //List<(int actId, int priceLimit, int discount, int number, int userLimit)> list = new();
        List<Activity> list = new();
        Dictionary<(int, int), int> dic = new();
        public DiscountSystem()
        {

        }

        public void AddActivity(int actId, int priceLimit, int discount, int number, int userLimit)
        {
            list.Add(new Activity {
                actId = actId,
                priceLimit = priceLimit,
                discount = discount,
                number = number,
                userLimit= userLimit
            });
        }

        public void RemoveActivity(int actId)
        {
            int i = Enumerable.Range(0, list.Count).First(i => list[i].actId == actId);
            list.RemoveAt(i);
        }

        bool CheckUserCount(Activity a, int u)
        {
            int used = 0;
            if (dic.ContainsKey((a.actId, u))) used = dic[(a.actId, u)];
            else dic[(a.actId, u)] = 0;
            return used < a.userLimit;
        }

        public int Consume(int userId, int cost)
        {
            var a = list.Where(t => t.priceLimit <= cost && t.number > 0 && CheckUserCount(t, userId)).OrderByDescending(t => t.discount).FirstOrDefault();
            if (a != null)
            {
                dic[(a.actId, userId)]++;
                a.number--;
                int ans = cost - a.discount;
                if (ans < 0) ans = 0;
                return ans;
            }
            return cost;
        }
    }

    [TestClass]
    public class 银联B
    {
        [TestMethod]
        public void Run()
        {
            int i = 10;
            Assert.AreEqual(10, i);
        }
    }
}
