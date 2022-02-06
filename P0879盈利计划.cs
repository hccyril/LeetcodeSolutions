using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 879 盈利计划 （hard）
     * 集团里有 n 名员工，他们可以完成各种各样的工作创造利润。

第 i 种工作会产生 profit[i] 的利润，它要求 group[i] 名成员共同参与。如果成员参与了其中一项工作，就不能参与另一项工作。

工作的任何至少产生 minProfit 利润的子集称为 盈利计划 。并且工作的成员总数最多为 n 。

有多少种计划可以选择？因为答案很大，所以 返回结果模 10^9 + 7 的值。

 

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/profitable-schemes
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0879盈利计划
    {
        // ver1 timeout
        // c=692206787
        //Total Time: 59844ms
        // 原因是用了List，效率低下
        // 现在改完之后突飞猛进：(79ms)
        //Success
        //Details
        //Runtime: 216 ms, faster than 100.00% of C# online submissions for Profitable Schemes.
        //Memory Usage: 26.4 MB, less than 100.00% of C# online submissions for Profitable Schemes.

        //class Counter
        //{
        //    const int COUNT_LIMIT = 1000000007;

        //    public int people;
        //    public int profit;
        //    public int count;

        //    public void AddCount(int count)
        //    {
        //        this.count = Reduce(this.count + count);
        //    }

        //    private static int Reduce(int count)
        //    {
        //        while (count > COUNT_LIMIT) count -= COUNT_LIMIT;
        //        return count;
        //    }

        //    public bool SameCounter(Counter c)
        //    {
        //        return c.people == this.people && c.profit == this.profit;
        //    }
        //}

        class CounterList// : List<Counter>
        {
            int nMaxPeopleCount;
            int profitLimit;

            int[] arr = new int[101 * 101];

            const int COUNT_LIMIT = 1000000007;
            public CounterList(int n, int p)
            {
                nMaxPeopleCount = n;
                profitLimit = p;
            }
            //public void Update(Counter c, int ng, int pf)
            //{
            //    if (c.people + ng <= nMaxPeopleCount)
            //    {
            //        UpdateCount(c.people + ng, c.profit + pf, c.count);
            //    }
            //    UpdateCount(c);
            //}

            //public void UpdateCount(Counter c)
            //{
            //    if (c.people > nMaxPeopleCount) return;
            //    if (c.profit > profitLimit) c.profit = profitLimit;
            //    foreach (var c0 in this)
            //    {
            //        if (c0.SameCounter(c))
            //        {
            //            c0.AddCount(c.count);
            //            return;
            //        }
            //    }
            //    this.Add(c);
            //}

            public int People;
            public int Profit;
            public int Count;
            private int Index;

            public void Last()
            {
                Index = 100 * 101 + 100;
                while (Index >= 0 && arr[Index] == 0) --Index;
                if (Index >= 0)
                    SetProperties();
            }

            public bool End()
            {
                return Index < 0;
            }

            public void MoveNext()
            {
                do {
                    --Index;
                } while (Index >= 0 && arr[Index] == 0);

                if (Index >= 0)
                    SetProperties();
            }

            private void SetProperties()
            {
                People = Index / 101;
                Profit = Index % 101;
                Count = arr[Index];
            }

            public void UpdateCount(int people, int profit, int count)
            {
                if (people <= nMaxPeopleCount)
                {
                    if (profit > profitLimit) profit = profitLimit;
                    int i = people * 101 + profit;
                    arr[i] += count;
                    if (arr[i] >= COUNT_LIMIT) arr[i] -= COUNT_LIMIT;
                }
                //Counter c = new Counter();
                //c.profit = profit;
                //c.people = people;
                //c.AddCount(count);
                //UpdateCount(c);
            }

            public int OutputResult()
            {
                int count = 0;
                for (int n = 0; n <= nMaxPeopleCount; ++n)
                {
                    int i = n * 101 + profitLimit;
                    if (arr[i] > 0)
                    {
                        count += arr[i];
                        if (count >= COUNT_LIMIT)
                            count -= COUNT_LIMIT;
                    }
                }
                return count;
                //Counter cR = new Counter();
                //foreach (var c in this)
                //    if (c.profit >= profitLimit)
                //        cR.AddCount(c.count);
                //return cR.count;
            }
        }

        public int ProfitableSchemes(int n, int minProfit, int[] group, int[] profit)
        {
            //var arr = (from i in Enumerable.Range(0, @group.Length)
            //           let people = @group[i]
            //           let pf = profit[i]
            //           orderby people descending
            //           select new
            //           {
            //               people,
            //               pf
            //           }).ToArray();

            CounterList list = new CounterList(n, minProfit);
            //CounterList l2 = new CounterList(n, minProfit);

            //foreach (var item in arr)
            for (int i = 0; i < group.Length; ++i)
            {
                int ng = group[i]; // item.people; //  n of group ppl
                int pf = profit[i]; // item.pf; //  profit

                
                for (list.Last(); !list.End(); list.MoveNext())
                {
                    list.UpdateCount(list.People + ng, list.Profit + pf, list.Count);
                }
                //foreach (var c0 in list)
                //{
                //    l2.Update(c0, ng, pf);
                //}

                list.UpdateCount(ng, pf, 1);
                //Counter c_new = new Counter();
                //c_new.people = ng;
                //c_new.profit = pf;
                //c_new.AddCount(1);
                //l2.UpdateCount(c_new);

                //CounterList temp = list;
                //list = l2;
                //l2 = temp;
                //l2.Clear();
            }

            list.UpdateCount(0, 0, 1);
            //list.Add(new Counter
            //{
            //    people = 0,
            //    profit = 0,
            //    count = 1
            //});

            return list.OutputResult();
        }
    }
}
