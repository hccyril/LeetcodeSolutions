using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium // 2021/11/08 Daily // Delayed till 2022/5/26 Done
    // 很简单的一题，可能是当时没时间看懂题目
    class P0299猜数字游戏
    {
        public string GetHint(string secret, string guess)
        {
            int bulls = 0;
            int[] ls = new int[10], lg = new int[10];
            foreach (int i in Enumerable.Range(0, secret.Length))
            {
                if (secret[i] == guess[i]) ++bulls;
                else
                {
                    ++ls[secret[i] - '0'];
                    ++lg[guess[i] - '0'];
                }
            }
            int cows = Enumerable.Range(0, 10).Sum(i => Math.Min(ls[i], lg[i]));
            return $"{bulls}A{cows}B";
        }
    }
}
