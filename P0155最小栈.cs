using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 数据结构题
    // 虽然是简单题，说实话算法我没想到。。
    // 这个技巧用得非常巧妙
    class P0155最小栈
    {
        public class MinStack
        {
            Stack<int[]> stk;

            public MinStack()
            {
                stk = new Stack<int[]>();
            }

            public void Push(int val)
            {
                int min = val;
                if (stk.Any() && stk.Peek()[1] < min) min = stk.Peek()[1];
                stk.Push(new int[] { val, min });
            }

            public void Pop()
            {
                stk.Pop();
            }

            public int Top()
            {
                return stk.Peek()[0];
            }

            public int GetMin()
            {
                return stk.Peek()[1];
            }
        }

        /**
         * Your MinStack object will be instantiated and called as such:
         * MinStack obj = new MinStack();
         * obj.Push(val);
         * obj.Pop();
         * int param_3 = obj.Top();
         * int param_4 = obj.GetMin();
         */
    }
}
