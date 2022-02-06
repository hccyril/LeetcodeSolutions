using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2021/12/16 daily
    // 几何题，跟游戏开发相关
    internal class P1610可见点的最大数目
    {
        const double fullAngle = Math.PI * 2.0;
        double CalcAngle(int x, int y)
        {
            if (x == 0) return y >= 0 ? Math.PI * 0.5 : Math.PI * 1.5;
            else if (y >= 0 && x > 0) return Math.Atan((double)y / x);
            else if (y >= 0 && x < 0) return Math.PI - Math.Atan(y / (x * -1.0));
            else if (y < 0 && x < 0) return Math.PI + Math.Atan((double)y / x);
            else return fullAngle - Math.Atan(-1.0 * y / x);
        }
        public int VisiblePoints(IList<IList<int>> points, int angle, IList<int> location)
        {
            double agl = Math.PI * angle / 180.0;
            List<double> list = new();
            int count = 0, org = 0; // org: 跟起点同一位置的点，必须特殊处理
            foreach (var p in points)
            {
                if (p[0] == location[0] && p[1] == location[1])
                {
                    org++;
                    continue;
                }
                var a = CalcAngle(p[0] - location[0], p[1] - location[1]);
                list.Add(a);
                list.Add(a + fullAngle);
            }
            list.Sort();
            count = 0;
            for (int head = 0, tail = 0; head < list.Count; ++head)
            {
                while (list[head] - list[tail] > agl) tail++;
                count = Math.Max(count, head - tail + 1);
            }
            return count + org;
        }

        internal static void Run()
        {
            IList<IList<int>> list = new List<IList<int>>();
            list.Add(new List<int>() { 2, 1 });
            list.Add(new List<int>() { 2, 2 });
            list.Add(new List<int>() { 3, 3 });
            var output = new P1610可见点的最大数目().VisiblePoints(list, 90, new List<int>() { 1, 1 });
            Console.WriteLine(output);
        }
    }
}
