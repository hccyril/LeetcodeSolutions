using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2034. 股票价格波动
    // hashheap
    // 还有红黑树的解法
    public class StockPrice
    {
        HashHeap maxHp, minHp;
        int currentTime = int.MinValue, currentPrice;
        public StockPrice()
        {
            maxHp = new(true);
            minHp = new(false);
        }

        public void Update(int timestamp, int price)
        {
            if (timestamp >= currentTime) // 写成 > 了！白白多了一个WA
            {
                currentTime = timestamp;
                currentPrice = price;
            }
            if (maxHp.ContainsKey(timestamp))
                maxHp.Remove(timestamp);
            maxHp.Push(timestamp, price);
            if (minHp.ContainsKey(timestamp))
                minHp.Remove(timestamp);
            minHp.Push(timestamp, price);
        }

        public int Current()
        {
            return currentPrice;
        }

        public int Maximum()
        {
            return maxHp.Head;
        }

        public int Minimum()
        {
            return minHp.Head;
        }
    }

    // ver 1: 随便做，超时
    //public class StockPrice
    //{
    //    SortedList<int, int> slist;
    //    public StockPrice()
    //    {
    //        slist = new();
    //    }

    //    public void Update(int timestamp, int price)
    //    {
    //        slist[timestamp] = price;
    //    }

    //    public int Current()
    //    {
    //        return slist.Last().Value;
    //    }

    //    public int Maximum()
    //    {
    //        return slist.Select(t => t.Value).Max();
    //    }

    //    public int Minimum()
    //    {
    //        return slist.Select(t => t.Value).Min();
    //    }
    //}

    internal class P2034股票价格波动
    {
        internal static void Run()
        {
            StockPrice sp = new();
            sp.Update(1, 10);
            sp.Update(2, 5);
            sp.Update(1, 3);
            Console.WriteLine(sp.Maximum());
        }
    }
}
