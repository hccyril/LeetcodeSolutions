using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium // 2021/11/13
    // 堆排序
    class P0739每日温度
    {
        // ver 2: 单调栈
        public int[] DailyTemperatures_Stack(int[] temperatures)
        {
            int[] ans = new int[temperatures.Length];
            Stack<int> stk = new();
            foreach (int i in Enumerable.Range(0, temperatures.Length))
            {
                while (stk.Any() && temperatures[i] > temperatures[stk.Peek()])
                {
                    int d = stk.Pop();
                    ans[d] = i - d;
                }
                stk.Push(i);
            }
            return ans;
        }

        class TempStruct
        {
            public int t; // temperature
            public int i; // index

            public void Deconstruct(out int t, out int i)
            {
                t = this.t; i = this.i;
            }
        }
        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] ans = new int[temperatures.Length];
            Heap<TempStruct> hp = new Heap<TempStruct>((a, b) => a.t < b.t);
            for (int i = 0; i < temperatures.Length; ++i)
            {
                TempStruct dt = new TempStruct { t = temperatures[i], i = i };
                while (hp.Any() && hp.Head.t < dt.t)
                {
                    (int t0, int i0) = hp.Pop();
                    ans[i0] = i - i0;
                }
                hp.Push(dt);
            }
            return ans;
        }
    }
}
