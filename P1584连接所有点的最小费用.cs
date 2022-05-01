using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/4/26 US Daily
    // MST 最小生成树
    internal class P1584连接所有点的最小费用
    {
        static int ManhattanDistance(int x1, int y1, int x2, int y2)
            => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        public int MinCostConnectPoints(int[][] points)
        {
            BitArray ba = new(points.Length);
            ba[0] = true;
            int count = 1, cost = 0;
            SortedSet<(int i, int j, int l)> hp = new(new MatrixComparer());
            for (int j = 1; j < points.Length; ++j)
                hp.Add((0, j, ManhattanDistance(points[0][0], points[0][1], points[j][0], points[j][1])));

            while (count < points.Length)
            {
                (int i, int j, int l) = hp.Min;
                hp.Remove(hp.Min);
                if (ba[j]) continue;
                ba[j] = true;
                cost += l;
                ++count;
                foreach (var k in Enumerable.Range(0, points.Length).Where(t => !ba[t]))
                    hp.Add((j, k, ManhattanDistance(points[j][0], points[j][1], points[k][0], points[k][1])));
            }
            return cost;
        }
    }
}
