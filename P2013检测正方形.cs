using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    public class DetectSquares
    {
        Dictionary<int, int>[] xps;

        public DetectSquares()
        {
            xps = new Dictionary<int, int>[1001];
            for (int i = 0; i <= 1000; ++i)
                xps[i] = new Dictionary<int, int>();
        }

        public void Add(int[] point)
        {
            int key = (point[0] << 10) | point[1];
            var sl = xps[point[0]];
            if (sl.ContainsKey(key)) ++sl[key];
            else sl[key] = 1;
        }

        public int Count(int[] point)
        {
            int x = point[0], y = point[1];
            int count = 0;
            foreach (int key in xps[x].Keys)
            {
                int x1 = key >> 10, y1 = key & ((1 << 10) - 1), c1 = xps[x][key];
                int side = Math.Abs(y1 - y); if (side == 0) continue;

                for (int x2 = x - side, i = 0; i < 2; ++i, x2 = x + side)
                {
                    if (x2 >= 0 && x2 <= 1000)
                    {
                        int y2 = y, key2 = (x2 << 10) | y2;
                        if (xps[x2].ContainsKey(key2))
                        {
                            int c2 = xps[x2][key2];
                            int x3 = x2, y3 = y1, key3 = (x3 << 10) | y3;
                            if (xps[x2].ContainsKey(key3))
                            {
                                int c3 = xps[x2][key3];
                                count += c1 * c2 * c3;
                            }
                        }
                    }
                }
            }
            return count;
        }
    }
    internal class P2013检测正方形
    {
    }
}
