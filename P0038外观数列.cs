using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0038外观数列
    {
        string Say(string str)
        {
            StringBuilder sb = new StringBuilder();
            char c = '0';
            int count = 0;
            foreach (var sc in str)
            {
                if (sc == c) count++;
                else
                {
                    if (count > 0)
                        sb.Append(count).Append(c);
                    c = sc;
                    count = 1;
                }
            }
            if (count > 0)
                sb.Append(count).Append(c);
            return sb.ToString();
        }
        public string CountAndSay(int n)
        {
            if (n == 1) return "1";
            return Say(CountAndSay(n - 1));
        }

        public static void Run()
        {
            for (int i = 1; i <= 6; ++i)
            {
                Console.WriteLine(new P0038外观数列().CountAndSay(i));
            }
        }
    }
}
