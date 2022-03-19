using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    // 银联专场：
    // https://leetcode-cn.com/contest/cnunionpay-2022spring/

    // 银联专场竞赛题解：
    // https://leetcode-cn.com/circle/discuss/YfxFdF/

    [TestClass]
    public class 银联A
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        bool Check(List<int> list, int i, int j, bool hasDelete)
        {
            if (i >= j) return true;
            if (list[i] == list[j]) return Check(list, i + 1, j - 1, hasDelete);
            else if (!hasDelete)
            {
                return Check(list, i + 1, j, true) || Check(list, i, j - 1, true);
            }
            else return false;
        }

        public bool IsPalindrome(ListNode head)
        {
            List<int> list = new();
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }
            return Check(list, 0, list.Count - 1, false);
        }

        [TestMethod]
        public void Run()
        {
            int i = 10;
            Assert.AreEqual(10, i);
        }
    }
}
