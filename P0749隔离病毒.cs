using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2022/7/18 Daily
// 2023/7/22 继续写，未能在45分钟内写完（当前已经写了60分钟）
// 2024/2/16 再重做
// 总结：模拟+细节处理
internal class P0749隔离病毒
{
    // 2024/02/16 重写 - AC
    public int ContainVirus(int[][] mx)
    {
        int m = mx.Length, n = mx[0].Length;
        int[] d = { 0, -1, 0, 1, 0 };

        // DFS
        (int, SortedSet<(int, int)>) DfsFill(int i, int j, int color)
        {
            Stack<(int, int, int)> stk = new();
            int orgColor = mx[i][j], nb = 0, x = i, y = j, z = 1;
            mx[i][j] = color;
            SortedSet<(int, int)> ss = new();
            while (z < 5 || stk.Any())
            {
                if (z == 5)
                {
                    (x, y, z) = stk.Pop();
                    continue;
                }
                int nextX = x + d[z - 1], nextY = y + d[z];
                if (nextX >= 0 && nextX < mx.Length && nextY >= 0 && nextY < mx[0].Length)
                {
                    if (mx[nextX][nextY] == orgColor)
                    {
                        mx[nextX][nextY] = color;
                        stk.Push((x, y, z + 1));
                        (x, y, z) = (nextX, nextY, 1);
                        continue;
                    }
                    else if (mx[nextX][nextY] == 0)
                    {
                        ++nb;
                        ss.Add((nextX, nextY));
                    }
                }
                ++z;
            }
            return (nb, ss);
        }

        int ans = 0, age = 1;
        while (mx.Any(r => r.Any(t => t == 0)) && mx.Any(r => r.Any(t => t > 0))) // 循环条件：存在未感染的区域以及存在未隔离的病毒
        {
            // a：(威胁的总数，隔离需要安装的防火墙数，左上角坐标x，左上角坐标y，威胁的所有格子数组)
            SortedSet<(int, int, int, int, SortedSet<(int, int)>)> a = new();

            // 计算每个感染区域的外围数量
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (mx[i][j] == age)
                    {
                        (var nb, var ss) = DfsFill(i, j, age + 1);
                        a.Add((ss.Count, nb, i, j, ss));
                    }

            // 将外围区域最大的进行隔离
            (_, int b, int x, int y, _) = a.Max;
            a.Remove(a.Max);
            DfsFill(x, y, -1);
            ans += b; // ++ans; // 注意题目描述

            // 剩下的区域病毒会扩散
            ++age;
            foreach ((_, _, _, _, var ss) in a)
                foreach ((int ni, int nj) in ss)
                    mx[ni][nj] = age;
        }
        return ans;
    }



    // 时间太久忘记了，准备重新写
    public int ContainVirus_20230722(int[][] isInfected)
    {
        int m = isInfected.Length, n = isInfected[0].Length;
        int[,] a = new int[m, n];
        int[] af = new int[2048]; // 威胁数

        void DfsFill(int i, int j, int c)
        {
            a[i, j] = c;
            foreach ((int x, int y) in isInfected.FourDir(i, j))
                if (isInfected[x][y] == 1 && a[x, y] == 0)
                    DfsFill(x, y, c);
                else if (isInfected[x][y] != 1 && isInfected[x][y] != -c)
                {
                    isInfected[x][y] = -c;
                    ++af[c];
                }
        }

        int c = 0;
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                if (isInfected[i][j] == 1 && a[i, j] == 0)
                    DfsFill(i, j, ++c);

        UnionFind uni = new(c + 1);

        while (true)
        {
            int d = Enumerable.Range(1, c).Max(t => af[t]);
            af[d] = 0;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    foreach ((int x, int y) in isInfected.FourDir(i, j))
                    {

                    }
                }
        }

        throw new NotImplementedException();
    }
}
