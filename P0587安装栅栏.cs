using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 凸包问题 // 2021/09/03 US Daily // 2021/11/17-18, 熬夜做了快三个小时终于搞定了！！
    /*
     * 587. 安装栅栏
在一个二维的花园中，有一些用 (x, y) 坐标表示的树。由于安装费用十分昂贵，你的任务是先用最短的绳子围起所有的树。只有当所有的树都被绳子包围时，花园才能围好栅栏。你需要找到正好位于栅栏边界上的树的坐标。
     
    # 最典型的凸包问题
     * */
    class P0587安装栅栏
    {
        static int CalcProduct(int x0, int y0, int px1, int py1, int px2, int py2)
        {
            int x1 = px1 - x0, y1 = py1 - y0;
            int x2 = px2 - x0, y2 = py2 - y0;
            return x1 * y2 - x2 * y1;
        }
        static (int x, int y) UnPair(int[] pair) => (pair[0], pair[1]);
        static int Key(int x, int y) => (x << 8) | y;
        static int Key(int[] p) => Key(p[0], p[1]);
        static (int x, int y) UnKeyPair(int key) => (key >> 8, key & 127);
        static int[] UnKey(int key) => new int[] { key >> 8, key & 127 };

        // 找到两端的顶点，然后将中间的点全部添加到skip
        (int max, int min) FindInBetween(List<int> slp, HashSet<int> skips, List<int> ans)
        {
            int max = -1, min = -1, max_x = -1, max_y = -1, min_x = -1, min_y = -1;
            foreach (var k in slp)
            {
                (int x, int y) = UnKeyPair(k);
                if (max < 0 || x > max_x || x == max_x && y > max_y)
                {
                    max = k; max_x = x; max_y = y;
                }
                if (min < 0 || x < min_x || x == min_x && y < min_y)
                {
                    min = k; min_x = x; min_y = y;
                }
            }
            foreach (var k in slp)
                if (k != max && k != min)
                {
                    skips.Add(k);
                //(int x, int y) = UnKeyPair(k); // debug
                //Console.WriteLine($"IBT ({x}, {y})"); // debug
                    ans.Add(k);
                }
            return (max, min);
        }

        public int[][] OuterTrees(int[][] trees)
        {
            if (trees.Length <= 3) return trees;

            List<int> ans = new List<int>(); // 存的格式：key
            HashSet<int> skips = new HashSet<int>(); // 处在答案直线上的中间点，必须从搜索中排除

            // 找到x最小的点作为起始点
            int tk0 = -1;
            int x0 = -1, y0 = -1;
            foreach (var tree in trees)
            {
                (int x, int y) = UnPair(tree);
                if (tk0 < 0 || x < x0 || x == x0 && y < y0)
                {
                    x0 = x; y0 = y; tk0 = Key(x, y);
                }
            }

            // 循环调用凸包
            List<int> slp = new List<int>();
            while (ans.Count == 0 || tk0 != ans[0])
            {
                //Console.WriteLine($"ADD ({x0}, {y0})"); // debug
                ans.Add(tk0);
                int next = -1, nx = -1, ny = -1;
                foreach (var tk in trees
                    .Select(t => Key(t))
                    .Where(k => k != tk0 && !skips.Contains(k)))
                {
                    bool update = true;
                    if (next >= 0)
                    {
                        update = false;
                        (int x, int y) = UnKeyPair(tk);
                        int prod = CalcProduct(x0, y0, nx, ny, x, y);
                        if (prod == 0)
                            slp.Add(tk);
                        else if (prod > 0)
                            update = true;
                    }
                    if (update)
                    {
                        (nx, ny) = UnKeyPair(next = tk); 
                        slp.Clear();
                    }
                }

                // 出现同一条线的答案时，要将中间的点移除
                if (slp.Count > 0)
                {
                    slp.Add(tk0); slp.Add(next);
                    (int max, int min) = FindInBetween(slp, skips, ans);
                    (nx, ny) = UnKeyPair(next = max != tk0 ? max : min); // 一直卡在这里(2)：更新完next忘了更新nx和ny
                }

                tk0 = next; x0 = nx; y0 = ny; // 一直卡在这里(1): 更新完tk0忘了更新x0和y0
            }

            // 输出
            return ans.Select(t => UnKey(t)).ToArray();
        }



        internal static void Run()
        {
            var input = new int[][] { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 2 }, new int[] { 2, 2 }, new int[] { 3, 2 }, new int[] { 3, 1 }, new int[] { 3, 0 }, new int[] { 2, 0 }, new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 3, 3 } };
            //new int[][] { new int[] { 1, 1 }, new int[] { 2, 2 }, new int[] { 2, 0 }, new int[] { 2, 4 }, new int[] { 3, 3 }, new int[] { 4, 2 } };
            var output = new P0587安装栅栏().OuterTrees(input);
            Console.WriteLine("count=" + output.Length);
            foreach (var p in output)
            {
                Console.WriteLine($"({p[0]}, {p[1]})");
            }
        }
    }
}
