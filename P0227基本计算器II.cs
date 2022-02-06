using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 经典题， stack
    internal class P0227基本计算器II
    {
        const char EMPTY = ' ';
        static int Level(char op)
        {
            if (op == '*' || op == '/') return 2;
            else if (op == '+' || op == '-') return 1;
            return 0;
        }
        class Token
        {
            public int num;
            public char op = EMPTY;
        }
        class Calculater
        {
            Stack<Token> stk = new();

            public void Push(int n, char c)
            {
                while (stk.Any() && Level(stk.Peek().op) >= Level(c))
                    n = Compute(n);
                stk.Push(new Token { num = n, op = c });

                // stupid old code
                //if (!stk.Any())
                //{
                //    stk.Push(new Token { num = n, op = c });
                //    return;
                //}

                //var op = stk.Peek().op;
                //if (Level(c) > Level(op)) // "a + b x "
                //{
                //    stk.Push(new Token { num = n, op = c });
                //}
                //else // "a + b + " => (a + b) +
                //{
                //    stk.Peek().num = Compute(stk.Peek().num, n, stk.Peek().op);
                //    stk.Peek().op = c;
                //}
            }

            public int Result(int n)
            {
                while (stk.Any())
                    n = Compute(n);
                //{
                //    var exp = stk.Pop();
                //    n = Compute(exp.num, n, exp.op);
                //}
                return n;
            }

            private int Compute(int n)
            {
                var exp = stk.Pop();
                int a = exp.num; 
                char op = exp.op;
                switch (op)
                {
                    case '+': return a + n;
                    case '-': return a - n;
                    case '*': return a * n;
                    case '/': return a / n;
                    default: return n;
                }
            }
        }

        public int Calculate(string s)
        {
            s = s.Replace(" ", "");
            int n = 0;
            Calculater calc = new();
            foreach (var c in s)
            {
                if (char.IsDigit(c)) n = n * 10 + (c - '0');
                else
                {
                    calc.Push(n, c);
                    n = 0;
                }
            }
            return calc.Result(n);
        }
    }
}
