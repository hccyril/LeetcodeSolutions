using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class WC281_B
	{
		public class ListNode
		{
			public int val;
			public ListNode next;
			public ListNode(int val = 0, ListNode next = null)
			{
				this.val = val;
				this.next = next;
			}
		}

		public ListNode MergeNodes(ListNode head) {
			var ans = head;
			for (var cur = head; cur.next != null; cur = cur.next) {
				if (cur.next.val != 0) {
					ans.val += cur.next.val;
				}
				else {
					ans.next = cur.next;
					ans = ans.next;
				}
			}
			for (var cur = head; cur != null; cur = cur.next) {
				if (cur.next != null && cur.next.val == 0) {
					cur.next = null;
					break;
				}
			}
			return head;
		}

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
