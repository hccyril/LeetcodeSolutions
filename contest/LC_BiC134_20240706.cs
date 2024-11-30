using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// https://leetcode.cn/contest/biweekly-contest-134/
// 晚了10分钟参加，1:09做完
// 最后两题值得一做，尤其是最后一题考察位运算和RMQ
internal class LC_BiC134_20240706
{
    #region Problem A
    // 本题相当于C题并固定k=3
    // 见C题代码
    #endregion

    #region Problem B
    // 脑筋急转弯，理解题意后不难发现最优解就是把后面敌人全部吸收，然后不断打倒能量最低的敌人刷分
    // 需要特殊处理一个敌人都无法打倒的情况（积分为0）
    public long MaximumPoints(int[] enemyEnergies, int currentEnergy)
    {
        Array.Sort(enemyEnergies);
        int n = enemyEnergies.Length;
        if (currentEnergy < enemyEnergies[0]) return 0;
        long ce = currentEnergy, pt = 0;
        for (int i = 1; i < n; ++i)
        {
            ce += enemyEnergies[i];
        }
        pt += ce / enemyEnergies[0];
        return pt;
    }
    #endregion

    #region Problem C
    // 令c[i]为到i为止的最长交替子串长度，c[i]可以从i-1叠加，然后判断c[i]>=k的话，就算一个解
    // 实际编码时滚动更新，因此用一个c维护就行了
    // 一个难点是这个数组是头尾连接的环，所以要从n-k位置开始计算，n-k, n-k+1, ... n-1, 0 这段要特殊处理
    public int NumberOfAlternatingGroups(int[] colors, int k)
    {
        int c = 1, n = colors.Length, ans = 0;

        // 从末尾一段开始统计
        for (int i = n - k + 2; i < n; ++i)
            if (colors[i] != colors[i - 1]) ++c;
            else c = 1;

        // 特殊处理c[0]（要从c[n-1]递推过来）
        if (colors[0] != colors[n - 1]) ++c;
        else c = 1;
        if (c == k) ++ans;

        // 接着正常处理c[1..n]即可
        for (int i = 1; i < n; ++i)
        {
            if (colors[i] != colors[i - 1]) ++c;
            else c = 1;
            if (c >= k) ++ans;
        }
        return ans;
    }
    #endregion

    #region Problem D

    // 备注：看到网上讨论很多人用ST表的模板求解，建议学习一下：
    // https://oi-wiki.org/ds/sparse-table/

    /* 数据范围1e9，也就是最多只有30位，我们把这30位分开进行处理统计
     * 1位情况：若k[b]==1 (k在第b位上的值为1)，那么子数组所有数都必须在这一位为1
     * 0位情况：若k[b]==0，那么子数组必须至少有一个数在这个位上是0
     * 对于1位情况，我们维护连续为1的最大长度即可，一旦出现0就重置
     * 所以难点在于，如何统计0位情况？
     * 我们看一个例子，假设有3个0位，有第3、4、5三个数分别在这三个0位为0，这样我们得到满足条件的子数组有
     * [3,4,5], [2,3,4,5]和[1,2,3,4,5]，但是[4,5]是不满足的
     * 总结发现新增的个数正好就是所有最后出现0位的最小值，也就是ans += min (具体见代码实现)
     * */
    public long CountSubarrays(int[] nums, int k)
    {
        // cn[b]维护当前位最后出现0的位置
        Span<int> cn = stackalloc int[30]; // C#特有的栈数组，使用上与数组一样，性能与C++数组持平
        // i维护最长连续长度，满足1位的值都是1
        int i = 0; long ans = 0;
        foreach (int x in nums)
        {
            int min = ++i;
            for (int b = 0; b < 30; ++b)
            {
                // k[b]==1，对于1位情况，只要等于1就可以继续i+1
                if ((k & 1 << b) != 0)
                {
                    // 出现0就必定不满足要求，重置所有信息从下一位开始重新统计
                    if ((x & 1 << b) == 0)
                    {
                        i = 0;
                        cn.Clear();
                        break;
                    }
                }
                // k[b]==0，找到min，使得从min到当前位至少有一个0
                else
                {
                    // 维护更新cn和min
                    if ((x & 1 << b) == 0)
                    {
                        cn[b] = i;
                    }
                    else
                    {
                        min = Math.Min(min, cn[b]);
                    }
                }
            }
            // min到当前位是满足条件的最小子数组，从1..min开头的子数组都是答案
            if (i > 0)
                ans += min;
        }
        return ans;
    }
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_BiC134_20240706 sln = new();

        return p switch
        {
            'A' => sln.RunTestA(),
            'B' => sln.RunTestB(),
            'C' => sln.RunTestC(),
            'D' => sln.RunTestD(),
            'E' => sln.RunTestE(),
            _ => -1
        };
    }

    int RunTestA()
    {
        return 0;
    }

    int RunTestB()
    {
        return 0;
    }

    int RunTestC()
    {
        return 0;
    }

    int RunTestD()
    {
        return 0;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
