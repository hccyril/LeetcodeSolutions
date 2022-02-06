using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 中位数是有序列表中间的数。如果列表长度是偶数，中位数则是中间两个数的平均值。

例如，

[2,3,4] 的中位数是 3

[2,3] 的中位数是 (2 + 3) / 2 = 2.5

设计一个支持以下两种操作的数据结构：

void addNum(int num) - 从数据流中添加一个整数到数据结构中。
double findMedian() - 返回目前所有元素的中位数。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/find-median-from-data-stream
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0295数据流的中位数
    {
        public class MedianFinder
        {
            HashHeap left;
            HashHeap right;
            static volatile int _key = 1;
            int GetKey() => _key++;

            /** initialize your data structure here. */
            public MedianFinder()
            {
                left = new HashHeap(true);
                right = new HashHeap(false);
            }

            public void AddNum(int num) => AddNum(GetKey(), num);

            public void AddNum(int key, int num)
            {
                bool isAddLeft = false;
                if (left.Count == 0 && right.Count == 0)
                    isAddLeft = true;
                else if (left.Count > 0)
                {
                    var leftHead = left.Head;
                    if (num < leftHead)
                        isAddLeft = true;
                }
                else if (right.Count > 0)
                {
                    var rightHead = right.Head;
                    if (num < rightHead)
                    {
                        isAddLeft = true;
                    }
                }
                
                if (isAddLeft) // add left
                {
                    left.Push(key, num);
                }
                else // add right
                {
                    right.Push(key, num);
                }
                Adjust();
            }

            public void Remove(int key)
            {
                if (left.ContainsKey(key))
                    left.Remove(key);
                else if (right.ContainsKey(key))
                    right.Remove(key);
            }

            void Adjust()
            {
                while (left.Count > right.Count + 1)
                {
                    var p = left.Pop();
                    right.Push(p.key, p.val);
                }
                while (right.Count > left.Count)
                {
                    var p = right.Pop();
                    left.Push(p.key, p.val);
                }
            }

            public double FindMedian()
            {
                if (left.Count == 0 && right.Count == 0) return 0;
                if (left.Count == right.Count) return (0.0 + left.Head + right.Head) / 2.0;
                return left.Head;
            }
        }
    }
}
