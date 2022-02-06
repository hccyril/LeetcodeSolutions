using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 背包问题->最短路径
    class P0638大礼包
    {
        /// <summary>
        /// 动态规划解法
        /// </summary>
        public int ShoppingOffersDP(IList<int> price, IList<IList<int>> special, IList<int> needs)
        {
            throw new NotImplementedException(); // 最短路径已AC，动态规划未实现
        }

        bool Complete(IList<int> buy, IList<int> needs)
        {
            for (int i = 0; i < needs.Count; ++i)
            {
                if (buy[i] != needs[i]) return false;
            }
            return true;
        }
        bool CanBuy(IList<int> buy, IList<int> offer, IList<int> needs)
        {
            for (int i = 0; i < needs.Count; ++i)
            {
                if (buy[i] + offer[i] > needs[i]) return false;
            }
            return true;
        }
        int Key(IList<int> buy)
        {
            int k = 0;
            for (int i = 0; i < buy.Count - 1; ++i)
                k = k * 11 + buy[i];
            return k;
        }
        public int ShoppingOffers(IList<int> price, IList<IList<int>> special, IList<int> needs)
        {
            int n = price.Count;
            int[] arr = new int[n + 1];
            for (int i = 0; i < n; ++i)
            {
                IList<int> offer = new List<int>(arr);
                offer[i] = 1;
                offer[n] = price[i];
                special.Add(offer);
            }

            HashSet<int> dic = new HashSet<int>();
            Heap<IList<int>> hp = new Heap<IList<int>>((a, b) => a.Last() < b.Last());
            hp.Push(new List<int>(arr));
            while (hp.Any())
            {
                var buy = hp.Pop();
                if (dic.Add(Key(buy)))
                {
                    if (Complete(buy, needs))
                        return buy.Last();
                    foreach (var offer in special)
                    {
                        if (CanBuy(buy, offer, needs))
                            hp.Push((from i in Enumerable.Range(0, n + 1)
                                     select buy[i] + offer[i]).ToList());
                    }
                }
            }

            return -1;
        }


    }
}
