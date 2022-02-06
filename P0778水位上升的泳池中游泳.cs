using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* https://leetcode.com/explore/challenge/card/june-leetcoding-challenge-2021/605/week-3-june-15th-june-21st/3785/
     *  Swim in Rising Water
On an N x N grid, each square grid[i][j] represents the elevation at that point (i,j).

Now rain starts to fall. At time t, the depth of the water everywhere is t. You can swim from a square to another 4-directionally adjacent square if and only if the elevation of both squares individually are at most t. You can swim infinite distance in zero time. Of course, you must stay within the boundaries of the grid during your swim.

You start at the top left square (0, 0). What is the least time until you can reach the bottom right square (N-1, N-1)?

Example 1:

Input: [[0,2],[1,3]]
Output: 3
Explanation:
At time 0, you are in grid location (0, 0).
You cannot go anywhere else because 4-directionally adjacent neighbors have a higher elevation than t = 0.

You cannot reach point (1, 1) until time 3.
When the depth of water is 3, we can swim anywhere inside the grid.
Example 2:

Input: [[0,1,2,3,4],[24,23,22,21,5],[12,13,14,15,16],[11,17,18,19,20],[10,9,8,7,6]]
Output: 16
Explanation:
 0  1  2  3  4
24 23 22 21  5
12 13 14 15 16
11 17 18 19 20
10  9  8  7  6

The final route is marked in bold.
We need to wait until time 16 so that (0, 0) and (4, 4) are connected.
Note:

2 <= N <= 50.
grid[i][j] is a permutation of [0, ..., N*N - 1].
     * */
    class Daily20210620
    {
        /**** version 1 ****/
        // !! wrong answer, 水不是只能往右和往后流，可以迂回地流
        //int[] dp = new int[50];
        //public int SwimInWater(int[][] grid)
        //{
        //    for (int i = 0; i < grid.Length; ++i)
        //    {
        //        for (int j = 0; j < grid[i].Length; ++j)
        //        {
        //            int up = i == 0 ? 0 : dp[j];
        //            int left = j == 0 ? 0 : dp[j - 1];
        //            dp[j] = Math.Max(Math.Min(up, left), grid[i][j]);
        //        }
        //    }
        //    return dp[grid.Length - 1];
        //}

        /**** version 2 - Modified Dijkstra ****/

        int[,] fill;

        void Init()
        {
            fill = new int[50, 50];
            for (int i = 0; i < 50; ++i)
                for (int j = 0; j < 50; ++j)
                    fill[i, j] = -1;
        }

        public int SwimInWater(int[][] grid)
        {
            int N = grid.Length;
            Init();

            SortedList<int, IList<int[]>> slist = new SortedList<int, IList<int[]>>();
            Queue<int[]> qu = new Queue<int[]>();
            int t = fill[0, 0] = grid[0][0];
            int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            qu.Enqueue(new int[] { 0, 0 });

            // start
            while (qu.Any() || slist.Any())
            {
                while (qu.Any())
                {
                    var arr = qu.Dequeue();
                    int x = arr[0], y = arr[1];
                    foreach (var di in directions)
                    {
                        int x1 = x + di[0], y1 = y + di[1];
                        if (x1 >= 0 && x1 < N && y1 >= 0 && y1 < N && fill[x1,y1] == -1)
                        {
                            int high = grid[x1][y1];
                            if (high <= t)
                            {
                                fill[x1, y1] = t;
                                if (x1 == N - 1 && y1 == N - 1) return t;
                                qu.Enqueue(new int[] { x1, y1 });
                            }
                            else
                            {
                                fill[x1, y1] = -2;
                                if (!slist.ContainsKey(high))
                                {
                                    slist.Add(high, new List<int[]>());
                                }
                                slist[high].Add(new int[] { x1, y1 });
                            }
                        }
                    }
                }

                // move t higher
                if (slist.Any())
                {
                    var kv = slist.First();
                    slist.RemoveAt(0);
                    t = kv.Key;
                    foreach (var p in kv.Value)
                    {
                        fill[p[0], p[1]] = t;
                        if (p[0] == N - 1 && p[1] == N - 1) return t;
                        qu.Enqueue(p);
                    }
                }
            }

            return -1;
        }




        internal static void Run()
        {
            //SortedList<int, string> sl = new SortedList<int, string>();
            //sl.Add(1, "str1");
            //sl.Add(1, "str2");
            //sl.Add(0, "str3");

            //Console.WriteLine(string.Join(",", sl.Values));

            int n = new Daily20210620().SwimInWater(new int[][]
            {
                new int[] {0, 1, 2, 3, 4 },
                new int[] { 24, 23, 22, 21, 5},
                new int[] { 12, 13, 14, 15, 16 },
                new int[] { 11, 17, 18, 19, 20 },
                new int[] { 10, 9, 8, 7, 6 }
            });
            Console.WriteLine(n);
        }
    }
}
