using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0932漂亮数组
    {
        /*
         * 方法二：加强版回溯
         */
        int N;
        int[] nums;
        int[,] dic;
        bool ans;
        void Rec(int[] list, int i)
        {
            if (i >= N)
            {
                ans = true;
                return;
            }
            for (int k = 1; !ans && k <= N; ++k)
            {
                if (nums[k] == 0)
                {
                    nums[k] = -1;
                    list[i] = k;
                    for (int j = 1; j <= N; ++j)
                    {
                        if (dic[k, j] > 0 && nums[j] >= 0 && nums[dic[k, j]] >= 0)
                            nums[dic[k, j]]++;
                    }
                    for (int ji = 0; ji < i; ++ji)
                    {
                        int j = list[ji];
                        if (dic[j, k] > 0 && nums[dic[j, k]] > 0)
                            nums[dic[j, k]]--;
                    }

                    Rec(list, i + 1); if (ans) return;

                    for (int ji = 0; ji < i; ++ji)
                    {
                        int j = list[ji];
                        if (dic[j, k] > 0 && nums[dic[j, k]] >= 0)
                            nums[dic[j, k]]++;
                    }
                    for (int j = 1; j <= N; ++j)
                    {
                        if (dic[k, j] > 0 && nums[j] >= 0 && nums[dic[k, j]] > 0)
                            nums[dic[k, j]]--;
                    }
                    nums[k] = 0;
                }
            }
        }
        public int[] BeautifulArray(int n)
        {
            N = n;
            ans = false;
            nums = new int[n + 1];
            dic = new int[n + 1, n + 1];
            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= n; ++j)
                {
                    if (i == j) continue;
                    int k = (j << 1) - i;
                    if (k > n) break;
                    else if (k > 0) dic[i, k] = j;
                }
            }
            int[] list = new int[n];
            Rec(list, 0);
            return list;
        }

        /*
         * 方法1：回溯（超时）
         * */
#if BAK
        int N;
        int[] arr;
        bool ans;
        void Rec(int[] list, int i)
        {
            if (i >= N)
            {
                ans = true;
                return;
            }
            for (int k = 0; !ans && k < N; ++k)
            {
                if (arr[k] == 0)
                {
                    arr[k] = i + 1;
                    list[i] = k + 1;
                    int d = (k + 1) << 1;
                    bool ok = true;
                    for (int j = 0; j < i; ++j)
                    {
                        int c = d - list[j];
                        if (c > 0 && c <= N && arr[c - 1] == 0)
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        Rec(list, i + 1);
                    }
                    arr[k] = 0;
                }
            }
        }
        public int[] BeautifulArray(int n)
        {
            N = n;
            ans = false;
            arr = new int[n];
            int[] list = new int[n];
            Rec(list, 0);
            return list;
        }
#endif
        public static void Run()
        {
            int input = 100; // 104734ms -> 72406ms
            var output = new P0932漂亮数组().BeautifulArray(input);
            Console.WriteLine(string.Join(' ', output));
        }
    }
}
