using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/12/10
    // 单调栈 + 记忆化回溯
    internal class P1340跳跃游戏V
    {
        public int MaxJumps(int[] arr, int d)
        {
            List<int> stk = new(arr.Length);
            List<int>[] paths = new List<int>[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                paths[i] = new();

                // 修改后的代码
                while (stk.Any() && arr[stk.Last()] < arr[i])
                {
                    if (stk.Last() + d >= i)
                        paths[i].Add(stk.Last());
                    stk.RemoveAt(stk.Count - 1);
                }
                for (int j = stk.Count - 1; j >= 0; --j)
                {
                    if (arr[stk[j]] == arr[i]) continue;
                    if (stk[j] + d >= i) paths[stk[j]].Add(i);
                    break;
                }

                // 初版代码有逻辑漏洞，主要是两个高度一样时要特殊考虑（造成两个WA）
                //while (stk.Any() && arr[stk.Last()] <= arr[i])
                //{
                //    if (arr[stk.Last()] < arr[i] && stk.Last() + d >= i) // 这里和下面的 >= 一开始写成 <= 了！！
                //        paths[i].Add(stk.Last());
                //    stk.RemoveAt(stk.Count - 1);
                //}
                //if (stk.Any() && stk.Last() + d >= i) paths[stk.Last()].Add(i);

                stk.Add(i);
            }

            int[] dp = new int[arr.Length];

            int DpDfs(int i)
            {
                if (dp[i] > 0) return dp[i];
                if (!paths[i].Any()) return dp[i] = 1;
                return dp[i] = paths[i].Select(j => DpDfs(j) + 1).Max();
            }

            return Enumerable.Range(0, arr.Length).Select(i => DpDfs(i)).Max();
        }

        internal static void Run()
        {
            //int[] input = { 6,4,14,6,8,13,9,7,10,6,12 };
            //int d = 2;

            // #case107 exp: 14 my: 13
            //int[] input = {12,90,68,50,48,41,19,83,70,80,69,48,69,55,85,61,80,19,89,11,14,15,23,93,56,30,7,32,66,71,10,86,86,60,6,14,26,3,71,86,93,40,59,85,20,83,87,85,91,26,92,93,96,29,35,49,21,91,10,1,46,63,22,97,58,59,14,61,4,72,78,58,51,45,36,5,71,48,31,93,87,48,21,69,73,93,38,62,68,93,84,67,57,86,36,70,46,1,54,38,24,73,57,42,54,13,68,36,61,84,72,36,9,57,63,17,29,48,42,78,98,36,72,51,75,92,81,40,90,80,36,73,84,1,96,60,95,99,72,58,84,64,49,65,1,35,93,79,48,45,22,81,75,49,59,71,58,38,23,14,84,78,65,60,65,85,10,37,67,84,79,34,38,16,16,41,72,30,20,44,31,43,37,90,84,5,10,59,33,88,80,25,66,61,72,31,17,94,20,52,35,37,45,20,52,78,98,2,41,23,95,86,10,64,49,13,82,93,55,13,64,93,49,88,82,47,47,60,74,93,17,29,57,62,7,71,91,61,45,90,73,36,88,84,11,36,58,38,97,80,27,61,52,85};
            //int d = 64;

            // #case108 exp: 12 my: 11
            int[] input = {83,11,83,70,75,45,96,11,80,75,67,83,6,51,71,64,64,42,70,23,11,24,95,65,1,54,31,50,18,16,11,86,2,48,37,34,65,67,4,17,33,70,16,73,57,96,30,26,56,1,16,74,82,77,82,62,32,90,94,33,58,23,23,65,70,12,85,27,38,100,93,49,96,96,77,37,69,71,62,34,4,14,25,37,70,3,67,88,20,30 };
            int d = 29;
            Console.WriteLine(new P1340跳跃游戏V().MaxJumps(input, d));
        }
    }
}

/* DEBUG
public class Solution {
    public int MaxJumps(int[] arr, int d)
    {
        List<int> stk = new(arr.Length);
        List<int>[] paths = new List<int>[arr.Length];
        for (int i = 0; i < arr.Length; ++i)
        {
            paths[i] = new();
            while (stk.Any() && arr[stk.Last()] <= arr[i])
            {
                if (arr[stk.Last()] < arr[i] && stk.Last() + d <= i)
                    paths[i].Add(stk.Last());
                stk.RemoveAt(stk.Count - 1);
            }
            if (stk.Any() && stk.Last() + d <= i) paths[stk.Last()].Add(i);
            stk.Add(i);
        }

        for (int i = 0; i < arr.Length; ++i)
            Console.WriteLine("{0}: {1}", i, string.Join(" ", paths[i]));

        int[] dp = new int[arr.Length];

        int DpDfs(int i)
        {
            if (dp[i] > 0) return dp[i];
            if (!paths[i].Any()) return dp[i] = 1;
            return dp[i] = paths[i].Select(j => DpDfs(j) + 1).Max();
        }

        int ans = Enumerable.Range(0, arr.Length).Select(i => DpDfs(i)).Max();
        Console.WriteLine(string.Join(" ", Enumerable.Range(0, arr.Length)));
        Console.WriteLine(string.Join(" ", dp));

        return ans;
    }
}
 * */