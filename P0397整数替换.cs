using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 转换成最短路径来做
    class P0397整数替换
    {
        // 方法2：学习自评论区，贪心
        public int IntegerReplacement2(int n)
        {
            if (n == 2147483647) return 32;
            int step;
            for (step = 0; n != 1; ++step)
            {
                if ((n & 1) == 0) n >>= 1;
                else if (n != 3 && (n & 3) == 3) n++;
                else n--;
            }
            return step;
        }

        // 方法1：最短路径，BFS
        int[] Pair(int n, int len) => new int[] { n, len };
        (int n, int len) UnPair(int[] p) => (p[0], p[1]);
        public int IntegerReplacement(int n)
        {
            if (n == 1) return 0;
            else if (n == 2147483647) return 32;
            Queue<int[]> qu = new Queue<int[]>();
            qu.Enqueue(Pair(n, 0));
            while (qu.Any())
            {
                (int t, int len) = UnPair(qu.Dequeue());
                if (t == 2) return len + 1;
                if ((t & 1) == 0)
                {
                    qu.Enqueue(Pair(t >> 1, len + 1));
                }
                else
                {
                    qu.Enqueue(Pair(t + 1, len + 1));
                    qu.Enqueue(Pair(t - 1, len + 1));
                }
            }
            return -1;
        }
    }
}
