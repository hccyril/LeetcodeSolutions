using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// from C++, for DEBUG
internal class LCP004
{
    int max_count = 0; // 最终答案

    // dp更新：当key不存在或者已保存的值不是更优时进行更新
    void update(Dictionary<int, int> d, int k, int v)
    {
        Console.WriteLine("  Update k={0} v={1}", k.ShowBits(), v);
        if (!d.TryGetValue(k, out var v0) || v0 < v)
        {             
            d[k] = v;
            if (v > max_count) max_count = v;
        }
    }

    public int domino(int n, int m, int[][] broken)
    {
        HashSet<int> broken_dict = new();
        foreach (var p in broken)
            broken_dict.Add(p[0] << m | p[1]);
        Dictionary<int, int> dict0 = new(), dict1 = new();
        Dictionary<int, int> dp0 = dict0, dp1 = dict1;
        int mask = (1 << m * 2) - 1;
        dp0[0] = 0;
        for (int row_i = 0; row_i < n; ++row_i)
            for (int col_i = 0; col_i < m; ++col_i)
            {
                Console.WriteLine("Row={0} Col={1}:", row_i, col_i);
                // 动态规划 - 状态转移
                foreach (var kv in dp0)
                {
                    int status_map = kv.Key, place_count = kv.Value; // 当前状态，以及对应状态已放置的最大骨牌数

                    // case#1: 当前格子什么都不放
                    int next_map = status_map << 2 & mask;
                    update(dp1, next_map, place_count);

                    // case#2: 当前格子放一块纵置骨牌(code=10)
                    bool can_place = true;
                    if (broken_dict.Contains(row_i << m | col_i)) // 当前格子已坏，不能放置
                        can_place = false;
                    else if (row_i > 0 && (2 << (m - 1 << 1) & status_map) != 0) // 上一行已经放了一个纵置，不能放置
                        can_place = false;
                    else if (col_i > 0 && (1 & status_map) != 0) // 左边已经放置了一个横置，不能放置
                        can_place = false;
                    if (can_place && row_i < n - 1 && !broken_dict.Contains(row_i + 1 << m | col_i))
                        update(dp1, next_map | 2, place_count + 1);

                    // case#3: 当前格子放置一块横置骨牌
                    if (col_i == m - 1) // 最右边没法放置
                        can_place = false;
                    else if (row_i > 0 && (2 << (m - 2 << 1) & status_map) != 0) // 右上角已经放置了纵向骨牌
                        can_place = false;
                    else if (broken_dict.Contains(row_i << m | col_i + 1)) // 右边格子已坏
                        can_place = false;
                    if (can_place)
                        update(dp1, next_map | 1, place_count + 1);
                }

                // {dp0, dp1} = {dp1, dp0}; // 交换dp0和dp1，高版本c++可以直接用pair交换
                Dictionary<int, int>  temp = dp0;
                dp0 = dp1;
                dp1 = temp;

                dp1.Clear();
            }
        return max_count;
    }

    internal static void Run()
    {
        var sln = new LCP004();
        var a = Array.Empty<int[]>();
        Console.WriteLine("LCP004_ans = " + sln.domino(3, 3, a));
    }
};