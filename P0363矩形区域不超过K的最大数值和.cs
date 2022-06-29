using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/5
    // SortedSet
    internal class P0363矩形区域不超过K的最大数值和
    {
        public const string 相似题 = "P0862和至少为K的最短子数组";

        public int MaxSumSubmatrix(int[][] matrix, int k)
        {
            foreach (var r in matrix)
                for (int i = 1; i < r.Length; ++i) r[i] += r[i - 1];

            SortedSet<int> sort = new();
            int max = int.MinValue;
            for (int end = 0; end < matrix[0].Length; ++end)
                for (int start = -1; start < end; ++start)
                {
                    int sum = 0;
                    sort.Clear();
                    foreach (int s in matrix.Select(r => r[end] - (start >= 0 ? r[start] : 0)))
                    {
                        sum += s;
                        //if (end == 5 && start == -1 && sum == 186) 
                        //    Console.WriteLine(sum);
                        if (sum <= k && sum > max) max = sum;
                        var filter = sort.GetViewBetween(sum - k, int.MaxValue);
                        if (filter.Any()) max = Math.Max(max, sum - filter.Min);
                        sort.Add(sum); // sort.Add(s); 查了这么久最后是这种弱智错误T_T|||
                    }
                }
            return max;
        }

        void BF(int[][] m, int k)
        {
            for (int i1 = 0; i1 < m.Length; ++i1)
                for (int i2 = i1; i2 < m.Length; ++i2)
                    for (int j1 = 0; j1 < m[0].Length; ++j1)
                        for (int j2 = j1; j2 < m[0].Length; ++j2)
                        {
                            int sum = 0;
                            for (int i = i1; i <= i2; ++i)
                                for (int j = j1; j <= j2; ++j)
                                    sum += m[i][j];
                            if (sum <= k)
                            {
                                Console.WriteLine("i: [{0}~{1}] j:[{2}~{3}] sum={4}", i1, i2, j1, j2, sum);
                            }
                        }
            /* 测试输出结果：
            i: [5~7] j:[0~2] sum=-110
            i: [5~7] j:[0~3] sum=-102
            i: [6~7] j:[0~5] sum=-101
            j:[0~5]的详细输出：
            30
            129
            191
            214
            264
            287  <-- i=5
            239
            186  <-- i=7
            259
            283
            335
            359
            418
            466
            508
             * */
        }

        internal static void Run()
        {
            // case 1: first WA soon solved
            // [[5,-4,-3,4],[-3,-4,4,5],[5,1,5,-4]] k=8

            // case 2: WA - 后来发现是低级错误
            //string s2 = "[[27,5,-20,-9,1,26,1,12,7,-4,8,7,-1,5,8],[16,28,8,3,16,28,-10,-7,-5,-13,7,9,20,-9,26],[24,-14,20,23,25,-16,-15,8,8,-6,-14,-6,12,-19,-13],[28,13,-17,20,-3,-18,12,5,1,25,25,-14,22,17,12],[7,29,-12,5,-5,26,-5,10,-5,24,-9,-19,20,0,18],[-7,-11,-8,12,19,18,-15,17,7,-1,-11,-10,-1,25,17],[-3,-20,-20,-7,14,-12,22,1,-9,11,14,-16,-5,-12,14],[-20,-4,-17,3,3,-18,22,-13,-1,16,-11,29,17,-2,22],[23,-15,24,26,28,-13,10,18,-6,29,27,-19,-19,-8,0],[5,9,23,11,-4,-20,18,29,-6,-4,-11,21,-6,24,12],[13,16,0,-20,22,21,26,-3,15,14,26,17,19,20,-5],[15,1,22,-6,1,-9,0,21,12,27,5,8,8,18,-1],[15,29,13,6,-11,7,-6,27,22,18,22,-3,-9,20,14],[26,-6,12,-10,0,26,10,1,11,-10,-16,-18,29,8,-8],[-19,14,15,18,-10,24,-9,-7,-19,-14,23,23,17,-5,6]]";
            //int k = -100;
            //var input = JsonConvert.DeserializeObject<int[][]>(s2);

            // case 3: TLE, 但是同样的代码再提交就过了，这边实测1800ms
            var input = Common.ReadInput<int[][]>(363);
            int k = 4030;

            var sln = new P0363矩形区域不超过K的最大数值和();
            //sln.BF(input, k);
            int ans = sln.MaxSumSubmatrix(input, k);

            Console.WriteLine(nameof(P0363矩形区域不超过K的最大数值和) + ": " + ans);
        }
    }
}
