using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 终极回溯题
    class P0282给表达式添加运算符
    {
        class ExpStruct
        {
            public string ex;
            public long val;
            public ExpStruct(string ex, long val)
            {
                this.ex = ex;
                this.val = val;
            }
        }
        string nums;
        List<ExpStruct>[] list;
        List<ExpStruct>[][] prods; // start - end - expstruct

        void AddProds(long n, int i0, int i)
        {
            if (i >= nums.Length) return;
            for (int inc = 0; inc < prods[i].Length; ++inc)
            {
                foreach (var es in prods[i][inc])
                    prods[i0][i + inc - i0].Add(new ExpStruct($"{n}*{es.ex}", n * es.val));
            }
        }
        void CalcProds(int i)
        {
            if (i + 1 < nums.Length) CalcProds(i + 1);
            prods[i] = new List<ExpStruct>[nums.Length - i];
            for (int j = 0; j < prods[i].Length; ++j) 
                prods[i][j] = new List<ExpStruct>();

            long n = nums[i] - '0';
            prods[i][0].Add(new ExpStruct(nums[i].ToString(), n));
            AddProds(n, i, i + 1);

            if (n > 0)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    n = n * 10 + (nums[j] - '0');
                    prods[i][j - i].Add(new ExpStruct(nums.Substring(i, j - i + 1), n));
                    AddProds(n, i, j + 1);
                }
            }
        }

        void AddOps(int i)
        {
            list[i].AddRange(prods[0][i]);
            for (int j, i1 = i + 1, inc = 0; (j = i1 + inc) < nums.Length; ++inc)
            {
                foreach (var es0 in list[i])
                    foreach (var es1 in prods[i1][inc])
                    {
                        list[j].Add(new ExpStruct($"{es0.ex}+{es1.ex}", es0.val + es1.val));
                        list[j].Add(new ExpStruct($"{es0.ex}-{es1.ex}", es0.val - es1.val));
                    }
            }
        }
        public IList<string> AddOperators(string num, int target)
        {
            nums = num;
            list = new List<ExpStruct>[num.Length];
            for (int i = 0; i < num.Length; ++i) list[i] = new List<ExpStruct>();
            prods = new List<ExpStruct>[nums.Length][];

            CalcProds(0);
//#if DEBUG
//            Console.WriteLine("prods");
//            foreach (var es in prods[0][num.Length - 1])
//                Console.WriteLine($"\"{es.ex}\" [{es.val}]");
//#endif
            for (int i = 0; i < num.Length; ++i)
                AddOps(i);

//#if DEBUG
//            Console.WriteLine("list");
//            foreach (var es in list[num.Length - 1])
//                Console.WriteLine($"\"{es.ex}\" [{es.val}]");
//#endif

            return list[num.Length - 1]
                .Where(t => t.val == target)
                .Select(t => t.ex)
                .ToList();
        }

        internal static void Run()
        {
            string input = "123"; int target = 6;
            var result = new P0282给表达式添加运算符().AddOperators(input, target);
            Console.WriteLine(string.Join('\n', result));
        }
    }
}
