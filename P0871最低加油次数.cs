using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, WC93-D // 2022/4/10
    // rank: 2074
    // DP: DP[i,j] 表示 到达加油站i，加了j次油，能行驶的最大公里数 // 注意！DP[i,j]不可加stations[i]的油，否则会出现重复加油的问题
    internal class P0871最低加油次数
    {
        // ver4: 堆解法，参考评论区后重写
        public int MinRefuelStops_Heap(int target, int startFuel, int[][] stations)
        {
            int re = 0;
            Heap<int> hp = new((a, b) => a > b);
            foreach (var st in stations.Append(new int[] { target, 0 }))
            {
                while (startFuel < st[0])
                {
                    if (!hp.Any()) return -1;
                    startFuel += hp.Pop();
                    ++re;
                }
                hp.Push(st[1]);
            }
            return re;
        }

        // ver3: 解决重复加油的问题：DP[i,j]不可包括stations[i]的油
        public int MinRefuelStops(int target, int startFuel, int[][] stations)
        {
            if (startFuel >= target) return 0;
            // stations = stations.OrderBy(s => s[0]).ToArray(); // 一开始没看到题目说已经排好序了
            int[,] dp = new int[stations.Length, stations.Length + 1];
            for (int i = 0; i < stations.Length; ++i)
                if (stations[i][0] > startFuel) break;
                else dp[i, 0] = startFuel;
            for (int re = 1; re <= stations.Length; ++re)
            {
                int reach = dp[0, re - 1];
                for (int i = 0; i < stations.Length; ++i)
                    if (reach >= stations[i][0])
                    {
                        dp[i, re] = reach;
                        if (dp[i, re - 1] >= stations[i][0]) // ver4: 少了这句会发生误加油情况
                        {
                            reach = Math.Max(reach, dp[i, re - 1] + stations[i][1]);
                            if (reach >= target) return re;
                        }
                    }
                    else break;
            }
            return -1;
        }

        // ver1 / ver2: WA
        public int MinRefuelStops_ver1_2(int target, int startFuel, int[][] stations)
        {
            if (startFuel >= target) return 0;
            stations = stations.OrderBy(s => s[0]).ToArray();
            int[,] dp = new int[stations.Length, stations.Length + 1];
            for (int i = 0; i < stations.Length; ++i)
                if (stations[i][0] > startFuel) break;
                else dp[i, 0] = startFuel;
            for (int re = 1; re <= stations.Length; ++re)
            {
                int reach = 0;
                for (int i = 0; i < stations.Length; ++i)
                {
                    if (dp[i, re - 1] >= stations[i][0])
                    {
                        reach = Math.Max(reach, dp[i, re - 1] + stations[i][1]);
                        if (reach >= target) return re;
                        dp[i, re] = reach;
                    }
                    // ver2: ver1漏了以下情况的考虑
                    else if (reach >= stations[i][0])
                    {
                        dp[i, re] = reach;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return -1;
        }
        // ver1: WA
        /* 
        // WA1: expected: 4, actual: 5
        1000
        83
        [[47,220],[65,1],[98,113],[126,196],[186,218],[320,205],[686,317],[707,325],[754,104],[781,105]]
        // WA2: expectd: -1, actual: 5
        1000
        83
        [[25,27],[36,187],[140,186],[378,6],[492,202],[517,89],[579,234],[673,86],[808,53],[954,49]]
        // WA3: expected 4, actual 3
        1000000000
        96590732
        [[84335497,55893340],[134469897,327446610],[163281207,258193765],[212462433,208075264],[300942558,45552547],[335043031,324767754],[372229714,23101233],[372328231,86876980],[398784199,494868436],[462048396,22152126],[580344894,464728027],[602916232,247912149],[610548716,170840030],[611098080,74489571],[651631124,448657787],[657021784,95198268],[692069550,335064997],[701732952,240834525],[732196530,75179777],[806512947,68270312],[836158357,90800339],[843153088,32825326],[865477067,494535323],[927663485,120424609],[954604705,474817587]]
         * */

        internal static void Run()
        {
            var sln = new P0871最低加油次数();
            int target = 1000, start = 83;
            var stations = new int[][] { new int[] { 47, 220 }, new int[] { 65, 1 }, new int[] { 98, 113 }, new int[] { 126, 196 }, new int[] { 186, 218 }, new int[] { 320, 205 }, new int[] { 686, 317 }, new int[] { 707, 325 }, new int[] { 754, 104 }, new int[] { 781, 105 } };
            Console.WriteLine(sln.MinRefuelStops_Heap(target, start, stations));
        }
    }
}
