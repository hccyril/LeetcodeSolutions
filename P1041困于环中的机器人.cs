using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 模拟+归纳 // 2022/1/9(US)
    internal class P1041困于环中的机器人
    {
        // 参考评论区之后再自己重写C#
        public bool IsRobotBounded(string instructions)
        {
            int[] v1 = { 0, 1 }, v2 = { 1, 0 }, v3 = { 0, -1 }, v4 = { -1, 0 };
            int[][] vecs = { v1, v2, v3, v4 };

            int x = 0, y = 0, dir = 0;
            foreach (char c in instructions)
            {
                if (c == 'G')
                {
                    x += vecs[dir][0];
                    y += vecs[dir][1];
                }
                else if (c == 'L')
                {
                    dir = (dir + 3) & 3;
                }
                else // if c == 'R'
                {
                    dir = (dir + 1) & 3;
                }
            }
            return dir != 0 ? true : x == 0 && y == 0;
        }
    }
}
