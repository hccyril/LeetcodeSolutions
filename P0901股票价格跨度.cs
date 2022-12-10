using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/10/21 Daily
    // 做了一天，发现是审错题，只需要用单调栈就好
    // 原题模板：
    //public class StockSpanner
    //{
    //    public StockSpanner()
    //    {
    //    }
    //    public int Next(int price)
    //    {
    //    }
    //}
    internal class P0901股票价格跨度
    {
        // 单调栈
        List<(int, int)> ms = new();
        public int Next(int p)
        {
            int c = 1;
            while (ms.Any() && ms.Last().Item1 <= p)
            {
                c += ms.Last().Item2;
                ms.RemoveAt(ms.Count - 1);
            }
            ms.Add((p, c));
            return c;
        }
    }
}
