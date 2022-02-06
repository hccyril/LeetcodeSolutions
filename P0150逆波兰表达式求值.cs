using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2021/8/23
    // 2022/2/4: ver2 switch用法
    internal class P0150逆波兰表达式求值
    {
        public int EvalRPN_ver2(string[] tokens)
        {
            static int Divide(int b, int a) => a / b;
            Stack<int> stk = new();
            Array.ForEach(tokens, t => stk.Push(t switch
            {
                "+" => stk.Pop() + stk.Pop(),
                "-" => -stk.Pop() + stk.Pop(),
                "*" => stk.Pop() * stk.Pop(),
                "/" => Divide(stk.Pop(), stk.Pop()),
                _ => int.Parse(t)
            }));
            return stk.Pop();
        }
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stk = new Stack<int>();
            foreach (var t in tokens)
            {
                if (t == "+")
                    stk.Push(stk.Pop() + stk.Pop());
                else if (t == "-")
                    stk.Push(-stk.Pop() + stk.Pop());
                else if (t == "*")
                    stk.Push(stk.Pop() * stk.Pop());
                else if (t == "/")
                {
                    int b = stk.Pop(), a = stk.Pop();
                    stk.Push(a / b);
                }
                else stk.Push(int.Parse(t));
            }
            return stk.Pop();
        }
    }
}
