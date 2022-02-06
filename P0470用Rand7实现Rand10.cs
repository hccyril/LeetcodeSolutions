using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0470用Rand7实现Rand10
    {
        static Random _rand = new Random();
        int Rand7()
        {
            return _rand.Next(7) + 1;
        }
        public int Rand10()
        {
            int a = Rand7(), b = 0;
            while (true)
            {
                b = Rand7();
                int x = (a - 1) * 7 + b;
                if (x <= 40) return x % 10 + 1;
                a = b;
            }
            return -1;
        }
        internal static void Run()
        {
            int[] cts = new int[11];
            int n = 100000;
            var obj = new P0470用Rand7实现Rand10();
            for (; n > 0; --n)
            {
                cts[obj.Rand10()]++;
            }
            for (n = 0; n < 11; ++n)
            {
                Console.WriteLine("{0}: {1}", n, cts[n]);
            }
        }
    }
}
