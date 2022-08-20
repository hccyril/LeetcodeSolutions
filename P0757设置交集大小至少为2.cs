using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/22 Daily
    // 又是有毒的一题，写来写去都不对
    // 07/22 13:30 到 07/23 01:12 总共提交了8次，7次WA（还不算没提交的测试）我哭死！！T_T	
    // 反思：其实ver3-ver4尝试过贪心法，跟最后过的贪心法很接近了，只是当时从后往前推，没想过从前往后推
    //       所以有时还是要尝试换个角度思考
    internal class P0757设置交集大小至少为2
    {
        // 看了题解之后受启发再做
        // 一遍就过了，还这么简单，我......
        public int IntersectionSizeTwo_REF(int[][] ivs)
        {
            ivs = ivs.OrderBy(iv => iv[1]).ThenByDescending(iv => iv[0]).ToArray();
            int b = ivs.First()[1], a = b - 1, cnt = 2;
            for (int i = 1; i < ivs.Length; ++i)
            {
                int l = ivs[i][0], r = ivs[i][1];
                if (l > b)
                {
                    cnt += 2;
                    b = r;
                    a = b - 1;
                }
                else if (l > a)
                {
                    ++cnt;
                    a = b;
                    b = r;
                }
            }
            return cnt;
        }

        // ver 13 - 前面6+6个版本都是错的！
        public int IntersectionSizeTwo(int[][] ivs)
        {
            Dictionary<(int, int), int[]> dic = new(); 
            foreach (var iv in ivs) dic[(iv[0], iv[1])] = iv;
            ivs = dic.Values.OrderBy(iv => iv[1]).ThenBy(iv => iv[0]).ToArray();

            int[] dp = new int[ivs.Length], dp2 = new int[ivs.Length]; 
            dp[0] = dp2[0] = 2;
            for (int i = 1; i < ivs.Length; ++i)
            {
                dp[i] = dp[i - 1] + 2;
                if (ivs[i - 1][0] >= ivs[i][0]) dp[i] = dp[i - 1];
                dp2[i] = dp[i - 1] + 2;
                if (ivs[i - 1][1] == ivs[i][1])
                    dp2[i] = Math.Min(dp2[i], dp2[i - 1] + 1);
                    //dp2[i] = Math.Min(dp2[i], dp2[i - 1]);
                else if (ivs[i - 1][1] >= ivs[i][0])
                    dp2[i] = Math.Min(dp2[i], dp2[i - 1] + 1);
                int a = ivs[i][0], b = ivs[i][1];
                int a2 = ivs[i - 1][0], b2 = ivs[i - 1][1];
                for (int j = i - 1; j >= 0; --j)
                {
                    if (ivs[j][1] >= ivs[i][0])
                    {
                        a2 = Math.Max(a2, ivs[j][0]);
                        b2 = Math.Min(b2, ivs[j][1]);
                        if (a2 < b2)
                        {
                            dp2[i] = Math.Min(dp2[i], j == 0 ? 3 : dp[j - 1] + 3);
                        }
                    }


                    if (ivs[j][1] < a)
                    {
                        dp[i] = Math.Min(dp[i], dp[j] + 2);
                        dp2[i] = Math.Min(dp2[i], dp[i] + 1);
                        if (b == ivs[i][1] && dp[i] < dp2[i]) dp2[i] = dp[i];
                        break;
                    }
                    else if (ivs[j][1] == a)
                    {
                        dp[i] = Math.Min(dp[i], Math.Min(dp[j] + 2, dp2[j] + 1));
                        dp2[i] = Math.Min(dp2[i], dp[i] + 1);
                        if (b == ivs[i][1] && dp[i] < dp2[i]) dp2[i] = dp[i];
                        //if (j == 0 || ivs[j - 1][1] < a)
                            break;
                    }
                    else //if (ivs[j][1] > a)
                    {
                        if (j == 0)
                        {
                            b = Math.Min(b, ivs[j][1]);
                            dp[i] = 2;
                            dp2[i] = Math.Min(dp2[i], dp[i] + 1);
                            if (b == ivs[i][1]) dp2[i] = 2;
                            break;
                        }
                        a = Math.Max(a, ivs[j][0]);
                        b = Math.Min(b, ivs[j][1]);
                        dp[i] = Math.Min(dp[i], Math.Min(dp[j] + 2, dp2[j] + 1));
                        dp2[i] = Math.Min(dp2[i], dp[i] + 1);
                        if (b == ivs[i][1] && dp[i] < dp2[i]) dp2[i] = dp[i];
                    }
                }
                /*DEBUG*/Console.WriteLine("{0} {1} {2} {3}", ivs[i][0], ivs[i][1], dp[i], dp2[i]);
            }
            return dp.Last();
        }
         
        // dp ver 1-6 (ver 7-12) all WA
        public int IntersectionSizeTwo_DP_WA(int[][] ivs)
        {
            ivs = ivs.OrderBy(iv => iv[1]).ThenBy(iv => iv[0]).ToArray();

            int[] dp = new int[ivs.Length], dp2 = new int[ivs.Length]; // dp2 要求区间最大值一定要取
            dp[0] = dp2[0] = 2;
            for (int i = 1; i < ivs.Length; ++i)
            {
                dp[i] = dp2[i] = dp[i - 1] + 2;
                int a = ivs[i][0], b = ivs[i][1];
                for (int j = i - 1; j >= 0; --j)
                {
                    if (ivs[j][1] == a)
                    {
                        dp[i] = Math.Min(dp[i], dp2[j] + 1);
                        dp2[i] = Math.Min(dp2[i], b == ivs[i][1] ? dp[i] : dp[i] + 1); // ver6
                        //dp2[i] = Math.Min(dp2[i], dp2[j] + 1); // ver5
                        break;
                    }
                    else if (ivs[j][1] > a)
                    {
                        a = Math.Max(a, ivs[j][0]);
                        b = Math.Min(b, ivs[j][1]);
                        dp[i] = Math.Min(dp[i], j == 0 ? 2 : dp[j - 1] + 2);
                        dp2[i] = b == ivs[i][1] ? dp[i] : Math.Min(dp2[i], dp[i] + 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return dp.Last();

            //// dp - ver4
            //int[] dp = new int[ivs.Length], dp2 = new int[ivs.Length]; // dp2 要求区间最大值一定要取
            //dp[0] = dp2[0] = 2;
            //for (int i = 1; i < ivs.Length; ++i)
            //{
            //    if (ivs[i][0] > ivs[i - 1][1])
            //    {
            //        dp[i] = dp2[i] = 2 + dp[i - 1];
            //    }
            //    else if (ivs[i][0] == ivs[i - 1][1])
            //    {
            //        dp[i] = dp2[i] = dp2[i - 1] + 1;
            //    }
            //    else
            //    {
            //        dp[i] = dp[i - 1];
            //        dp2[i] = ivs[i - 1][1] == ivs[i][1] ? dp[i] : dp[i] + 1;
            //    }
            //    Console.WriteLine("{0} {1} {2} {3}", ivs[i][0], ivs[i][1], dp[i], dp2[i]);
            //}
            //return dp.Last();

            // dp - ver3
            //int[] dp = new int[ivs.Length], dp2 = new int[ivs.Length]; // dp2 要求区间最大值一定要取
            //dp[0] = dp2[0] = 2;
            //for (int i = 1; i < ivs.Length; ++i)
            //{
            //    dp[i] = dp2[i] = dp[i - 1] + 2;
            //    int a = ivs[i][0], b = ivs[i][1];
            //    for (int j = i - 1; j >= 0 && ivs[j][1] >= a; --j)
            //    {
            //        if (ivs[j][1] == a)
            //        {
            //            dp[i] = dp2[j] + 1;

            //            break;
            //        }
            //        else
            //        {
            //            dp[i] = dp[j];
            //            if (ivs[j][1] == ivs[i][1])
            //                dp2[i] = dp2[j];
            //            else
            //                dp2[i] = Math.Min(dp2[i], dp[j] + 1);
            //        }
            //    }
            //}
            //return dp.Last();

            // dp - ver2
            //int[] dp = new int[ivs.Length], dp2 = new int[ivs.Length];
            //dp[0] = dp2[0] = 2;
            //for (int i = 1; i < ivs.Length; ++i)
            //{
            //    dp[i] = dp2[i] = dp[i - 1] + 2;
            //    for (int j = i - 1; j >= 0 && ivs[j][1] >= ivs[i][0]; --j)
            //    {
            //        if (ivs[j][1] == ivs[i][0])
            //        {
            //            dp[i] = dp2[i] = dp2[j] + 1;
            //        }
            //        else
            //        {
            //            dp[i] = dp[j];
            //            if (ivs[j][1] == ivs[i][1])
            //                dp2[i] = dp2[j];
            //            else
            //                dp2[i] = Math.Min(dp2[i], dp[j] + 1);
            //        }
            //    }
            //}

            //return dp.Last();

            // dp - ver1
            //int[] dp1 = new int[ivs.Length], dp2 = new int[ivs.Length];
            //dp1[0] = 1; dp2[0] = 2;
            //for (int i = 1; i < ivs.Length; ++i)
            //{
            //    dp1[i] = dp2[i - 1] + 1; dp2[i] = dp2[i - 1] + 2;
            //    int a = ivs[i][0], b = ivs[i][1];
            //    for (int j = i - 1; j >= 0 && ivs[j][1] >= a; --j)
            //    {
            //        if (ivs[j][1] == a)
            //        {

            //        }
            //        else
            //        {
            //            a = Math.Max(a, ivs[j][0]);
            //            b = Math.Min(b, ivs[j][1]);
            //            dp2[i] = Math.Min(dp2[i], 2 + (j == 0 ? 0 : dp2[j - 1]));
            //            dp1[i] = ???
            //        }
            //    }

            //    int pre = i - 1, need = 2;
            //    while (pre >= 0 && ivs[pre][1] >= ivs[i][0])
            //    {
            //        if (ivs[pre][1] == ivs[i][0])
            //            need = 3;
            //        --pre;
            //    }
            //    dp[i] = need + (pre >= 0 ? dp[pre] : 0);
            //}

        }

        // 写了6个版本都是错的，估计方向错了
        public int IntersectionSizeTwo_WA(int[][] intervals)
        {
            intervals = intervals.OrderBy(iv => iv[1]).ThenBy(iv => iv[0]).ToArray();
            //var a = intervals.Select(iv => iv[1]).ToArray();
            int[] dp = new int[intervals.Length];
            dp[0] = 2;
            //for (int i = 1; i < intervals.Length; ++i)
            //{
            //    // ver2 WA
            //    int pre = i - 1, need = 2;
            //    while (pre >= 0 && intervals[pre][1] >= intervals[i][0])
            //    {
            //        if (intervals[pre][1] == intervals[i][0])
            //            need = 3;
            //        --pre;
            //    }
            //    dp[i] = need + (pre >= 0 ? dp[pre] : 0);

            //    // ver1 WA
            ////if (intervals[i][0] > intervals[i - 1][1])
            ////    dp[i] = 2 + dp[i - 1];
            ////else if (intervals[i][0] == intervals[i - 1][1])
            ////    dp[i] = 3 + (i > 1 ? dp[i - 2] : 0);
            ////else
            ////    dp[i] = 2 + (i > 1 ? dp[i - 2] : 0);
            //}

            // ver3 直接推
            // ver4 加上bFix
            int cnt = 2, a = intervals.Last()[0], b = intervals.Last()[1];
            bool bFix = false;
            for (int i = intervals.Length - 1; i >= 0; --i)
            {
                if (a < intervals[i][1]) // && b > intervals[i][0] 不需要，因为右边界已排序
                {
                    a = Math.Max(a, intervals[i][0]);
                    if (bFix)
                    {
                        if (intervals[i][1] < b)
                        {
                            cnt += 1;
                            // ver6
                            b = a;
                            a = intervals[i][0];
                            //bFix = false;
                            ////b = intervals[i][1];
                            //(a, b) = (intervals[i][0], intervals[i][1]); // ver 5 数值调整
                        }
                    }
                    else
                    {
                        b = Math.Min(b, intervals[i][1]);
                    }
                }
                else if (a == intervals[i][1])
                {
                    cnt += 1;
                    b = a;
                    bFix = true;
                    a = intervals[i][0];
                }
                else // a > intervals[i][1]
                {
                    cnt += 2;
                    a = intervals[i][0];
                    b = intervals[i][1];
                }

                /*/DEBUG*/
                Console.WriteLine("[{0} {1}] ({2}) a={3} b={4}", intervals[i][0], intervals[i][1], cnt, a, b);
            }
            return cnt;

            //return dp.Last();
        }

        /* ALL Test cases
        [[0,3],[0,4],[0,9],[8,9],[0,7],[1,4],[6,10],[0,4],[3,7],[6,8]]
        [[1,2],[2,3],[2,4],[4,5]]
        [[1,3],[1,4],[2,5],[3,5]]
        [[2,15],[9,17],[0,6],[17,25],[0,25]]
        [[39,88],[50,74],[6,34],[32,33],[71,95],[89,96],[3,96],[60,77],[17,37],[21,60],[25,44],[8,76],[20,80],[5,19],[18,45],[76,85],[54,69],[40,45],[71,86],[31,53]]
        [[1,3],[3,7],[5,7],[7,8]]
        [[2,10],[3,7],[3,15],[4,11],[6,12],[6,16],[7,8],[7,11],[7,15],[11,12]]
        [[7,16],[3,12],[7,16],[2,15],[14,19]]
         * */
        internal static void Run()
        {
            // input 4: exp 5, my 6
            //string json = "[[0,3],[0,4],[0,9],[8,9],[0,7],[1,4],[6,10],[0,4],[3,7],[6,8]]";
            //var input = JsonConvert.DeserializeObject<int[][]>(json);
            //*Test*/ input = input.OrderBy(iv => iv[1]).ThenBy(iv => iv[0]).Take(8).ToArray();

            // iuput?? exp 5 my 4
            //var input = JsonConvert.DeserializeObject<int[][]>("[[1,3],[3,7],[5,7],[7,8]]");

            // iuput?? exp 5 my 4
            //var input = JsonConvert.DeserializeObject<int[][]>("[[2,10],[3,7],[3,15],[4,11],[6,12],[6,16],[7,8],[7,11],[7,15],[11,12]]");

            // input ?? exp 4 my 3
            var input = JsonConvert.DeserializeObject<int[][]>("[[7,16],[3,12],[7,16],[2,15],[14,19]]");

            // sample case 2
            //int[] i1 = { 1, 3 }, i2 = { 1, 4 }, i3 = { 2, 5 }, i4 = { 3, 5 };
            //int[][] input = { i1, i2, i3, i4 };

            // input1 - fix
            //var input = new int[][] { new int[] { 2, 15 }, new int[] { 9, 17 }, new int[] { 0, 6 }, new int[] { 17, 25 }, new int[] { 0, 25 } };

            // input2 - my: 14 exp: 12
            //var input = new int[][] { new int[] { 39, 88 }, new int[] { 50, 74 }, new int[] { 6, 34 }, new int[] { 32, 33 }, new int[] { 71, 95 }, new int[] { 89, 96 }, new int[] { 3, 96 }, new int[] { 60, 77 }, new int[] { 17, 37 }, new int[] { 21, 60 }, new int[] { 25, 44 }, new int[] { 8, 76 }, new int[] { 20, 80 }, new int[] { 5, 19 }, new int[] { 18, 45 }, new int[] { 76, 85 }, new int[] { 54, 69 }, new int[] { 40, 45 }, new int[] { 71, 86 }, new int[] { 31, 53 } };
            var ans = new P0757设置交集大小至少为2().IntersectionSizeTwo(input);
            Console.WriteLine(ans);
        }
    }
}
