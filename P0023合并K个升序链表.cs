using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // ver1 2021/7/16 两两合并 216ms
    // ver2 2022/2/5 堆排序 212ms
    internal class P0023合并K个升序链表
    {
        #region ver2 - 堆排序
        public ListNode MergeKLists(ListNode[] lists)
        {
            ListNode hh = new(), ln = hh;
            Heap<ListNode> hp = new((a, b) => a.val < b.val);
            foreach (ListNode l in lists) hp.Push(l);
            while (hp.Any())
            {
                ln.next = hp.Pop(); 
                ln = ln.next;
                if (ln.next != null) hp.Push(ln.next);
            }
            return hh.next;
        }
        #endregion 

        #region ver1 - 两两合并
        public ListNode MergeKLists_ver1(ListNode[] lists)
        {
            ListNode ans = null;
            for (int i = 0; i < lists.Length; ++i)
            {
                ans = MergeTwoLists(ans, lists[i]);
            }
            return ans;
        }

        public ListNode MergeTwoLists(ListNode a, ListNode b)
        {
            if (a == null || b == null)
            {
                return a != null ? a : b;
            }
            ListNode head = new ListNode(0);
            ListNode tail = head, aPtr = a, bPtr = b;
            while (aPtr != null && bPtr != null)
            {
                if (aPtr.val < bPtr.val)
                {
                    tail.next = aPtr;
                    aPtr = aPtr.next;
                }
                else
                {
                    tail.next = bPtr;
                    bPtr = bPtr.next;
                }
                tail = tail.next;
            }
            tail.next = (aPtr != null ? aPtr : bPtr);
            return head.next;
        }
        #endregion 
    }
}
