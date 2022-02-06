using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2022/1/28 Daily
    // BFS
    internal class P1765地图中的最高点
    {
        // ver2: improved BFS (968ms)
        public int[][] HighestPeak(int[][] isWater)
        {
            for (int i = 0; i < isWater.Length; ++i)
                for (int j = 0; j < isWater[i].Length; ++j)
                    isWater[i][j] = isWater[i][j] == 1 ? 0 : -1;
            Queue<(int i, int j)> qu = new();
            for (int i = 0; i < isWater.Length; ++i)
                for (int j = 0; j < isWater[i].Length; ++j)
                    if (isWater[i][j] == -1 && BesideWater(isWater, i, j))
                    {
                        isWater[i][j] = 1;
                        qu.Enqueue((i, j));
                    }
            while (qu.Any())
            {
                (int i, int j) = qu.Dequeue();
                int next = isWater[i][j] + 1;
                foreach ((int ni, int nj) in Around(isWater, i, j))
                    if (isWater[ni][nj] == -1)
                    {
                        isWater[ni][nj] = next;
                        qu.Enqueue((ni, nj));
                    }
            }
            return isWater;
        }

        bool BesideWater(int[][] isWater, int i, int j)
            => Around(isWater, i, j).Any(t => isWater[t.ni][t.nj] == 0);

        IEnumerable<(int ni, int nj)> Around(int[][] isWater, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < isWater.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < isWater[i].Length - 1) yield return (i, j + 1);
        }

        // ver1: simple BFS - TLE (73344ms)
        public int[][] HighestPeak_V1_TLE(int[][] isWater)
        {
            Queue<(int i, int j)> qu = new();
            for (int i = 0; i < isWater.Length; ++i)
                for (int j = 0; j < isWater[i].Length; ++j)
                    if (isWater[i][j] == 1)
                        isWater[i][j] = 0;
                    else
                    {
                        isWater[i][j] = 1;
                        qu.Enqueue((i, j));
                    }
            while (qu.Any())
            {
                (int i, int j) = qu.Dequeue();
                int min = int.MaxValue;
                if (i > 0) min = Math.Min(min, isWater[i - 1][j]);
                if (i < isWater.Length - 1) min = Math.Min(min, isWater[i + 1][j]);
                if (j > 0) min = Math.Min(min, isWater[i][j - 1]);
                if (j < isWater[i].Length - 1) min = Math.Min(min, isWater[i][j + 1]);
                if (min + 1 >= isWater[i][j] + 1)
                {
                    ++isWater[i][j];
                    qu.Enqueue((i, j));
                }
            }
            return isWater;
        }

        internal static void Run()
        {
            var sln = new P1765地图中的最高点();
            int[][] input = new int[1000][];
            for (int i = 0; i < 1000; ++i)
                input[i] = new int[1000];
            input[0][0] = 1;
            var output = sln.HighestPeak(input);
            Console.WriteLine(output[999][999]);
        }
    }
}
