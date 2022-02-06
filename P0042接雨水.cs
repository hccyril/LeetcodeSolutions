using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0042接雨水
    {
        public int Trap(int[] height)
        {
            Stack<int[]> stk = new Stack<int[]>();
            int water = 0;
            for (int i = 0; i < height.Length; ++i)
                if (height[i] > 0)
                {
                    int floor = 0;

                    while (stk.Any() && floor < height[i])
                    {
                        var d = stk.Peek();
                        int h = Math.Min(d[0], height[i]);
                        water += (h - floor) * (i - d[1] - 1);
                        floor = h;
                        if (d[0] <= height[i]) stk.Pop();
                    }

                    stk.Push(new int[] { height[i], i });
                }
            return water;
        }
    }
}
