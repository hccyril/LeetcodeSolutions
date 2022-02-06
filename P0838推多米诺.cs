using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0838推多米诺
    {
        public string PushDominoes(string dominoes)
        {
            int[] arr = new int[dominoes.Length];
            int r = 0;
            for (int i = 0; i < dominoes.Length; ++i)
            {
                if (dominoes[i] == 'R')
                    arr[i] = r = 1;
                else if (dominoes[i] == 'L')
                {
                    arr[i] = r = -1;
                    for (int j = i - 1; j >= 0; --j)
                    {
                        int sum = arr[j] + --r;
                        if (arr[j] == 0 || sum > 0)
                        {
                            arr[j] = r;
                        }
                        else
                        {
                            if (sum == 0) arr[j] = 0;
                            break;
                        }
                    }
                    r = 0;
                }
                else // '.' 
                {
                    if (r > 0) arr[i] = ++r;
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (var a in arr)
            {
                sb.Append(a > 0 ? 'R' : a < 0 ? 'L' : '.');
            }
            return sb.ToString();
        }

        public static void Run()
        {
            string input = "RR.L";
            string output = new P0838推多米诺().PushDominoes(input);
            Console.WriteLine(output);
        }
    }
}
