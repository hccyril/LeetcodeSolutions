using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// medium, 2024/2/6 Daily
// LCP 30. 魔塔游戏
// ref 《871. 最低加油次数》
internal class LCP030
{
    public int MagicTower(int[] nums)
    {
        if (nums.Sum() < 0) return -1;
        PriorityQueue<int, int> hp = new(nums.Count(t => t < 0));
        int ans = 0;
        long lp = 1L; // 又玩这么阴的！
        for (int i = 0; i < nums.Length; ++i)
        {
            int t = nums[i];
            if (t < 0)
            {
                hp.Enqueue(i, t);
                lp += t;
                while (lp <= 0)
                {
                    int j = hp.Dequeue();
                    lp -= nums[j];
                    ++ans;
                }
            }
            else
            {
                lp += t;
            }
        }
        return ans;
    }

    internal static void Run()
    {
        int[] input = { 100, 100, 100, -250, -60, -140, -50, -50, 100, 150 };
        var sln = new LCP030();
        Console.WriteLine(sln.MagicTower(input));
    }
}
