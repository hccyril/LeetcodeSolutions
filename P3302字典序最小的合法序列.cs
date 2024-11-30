using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// medium, 2024/10/1
// 双周赛140-C
// 前后缀分解
internal class P3302字典序最小的合法序列
{
    public int[] ValidSequence(string word1, string word2)
    {
        int n = word1.Length, m = word2.Length;
        Span<int> pre = stackalloc int[n];
        Span<int> suf = stackalloc int[n];
        int c = 0;
        for (int i = 0; i < n; ++i)
            if (c < m && word1[i] == word2[c])
                pre[i] = ++c;
            else
                pre[i] = c;

        c = 0;
        for (int i = n - 1; i >= 0; --i)
            if (c < m && word1[i] == word2[^(c + 1)])
                suf[i] = ++c;
            else
                suf[i] = c;

        int mid = -1;
        for (int i = 0; i < n; ++i)
        {
            c = 0;
            if (i > 0) c += pre[i - 1];
            if (i < n - 1) c += suf[i + 1];
            if (c >= m - 1)
            {
                mid = i;
                break;
            }
        }

        if (mid < 0) return Array.Empty<int>();

        int[] ans = new int[m];
        c = 0;
        int j = 0;
        if (mid > 0 && pre[mid - 1] > 0)
            for (int i = 0; i < mid; ++i)
                if (pre[i] > c)
                {
                    ans[j++] = i;
                    ++c;
                }

        // fix WA2: 有可能mid对应的位置刚好和对应word2的字符相等，这时要顺次往后移位
        while (c < m && word1[mid] == word2[c])
        {
            ans[j++] = mid++;
            ++c;
        }
        if (c < m)
        {
            ans[j++] = mid;
            ++c;
        }
        //ans[j++] = mid;
        //++c;

        // fix WA1: 题意要找到字典序最小下标
        for (int i = mid + 1; i < n && c < m; ++i)
        {
            if (word1[i] == word2[c])
            {
                ans[j++] = i;
                ++c;
            }
        }
        //c = 0; j = m - 1;
        //if (mid < n - 1 && suf[mid + 1] > 0)
        //    for (int i = n - 1; i > mid; --i)
        //        if (suf[i] > c)
        //        {
        //            ans[j--] = i;
        //            ++c;
        //        }

        return ans;
    }

    internal static void Run()
    {
        var sln = new P3302字典序最小的合法序列();
        Console.WriteLine(string.Join(" ", sln.ValidSequence(
            "ghhgghhhhhh", "gg" // WA2
            //"abc", "ab" 
            )));
    }
}
