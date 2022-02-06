using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, backtrack （回溯）
    internal class P0241为运算表达式设计优先级
    {
        IList<int> ansList;
        public IList<int> DiffWaysToCompute(string expression)
        {
            ansList = new List<int>();
            List<(int x, char op)> tokens = new();
            int x = 0; char op = '/';
            foreach (var c in expression)
                switch (c)
                {
                    case '+':
                    case '-':
                    case '*':
                        op = c;
                        tokens.Add((x, op));
                        x = 0;
                        break;
                    default:
                        x = x * 10 + c - '0';
                        break;
                }
            if (!tokens.Any()) return new List<int> { int.Parse(expression) };
            tokens.Add((x, '/'));
            Stack<(int x, char op)> stk = new();
            (x, op) = tokens[0];
            Dfs(stk, x, op, tokens, 1);
            return ansList;
        }

        void Dfs(Stack<(int x, char op)> stk, int x, char op, List<(int x, char op)> tokens, int i)
        {
            if (op == '/')
            {
                Calc(stk, x);
                return;
            }

            if (stk.Any())
            {
                (int a, char c) = stk.Pop();
                Dfs(stk, Exp(a, x, c), op, tokens, i);
                stk.Push((a, c));
            }

            stk.Push((x, op));
            (x, op) = tokens[i];
            Dfs(stk, x, op, tokens, i + 1);
            stk.Pop();
        }

        private void Calc(Stack<(int x, char op)> stk, int val)
        {
            if (!stk.Any()) { ansList.Add(val); return; }
            (int x, char op) = stk.Pop();
            Calc(stk, Exp(x, val, op));
            stk.Push((x, op));
        }

        private int Exp(int x, int y, char op)
        {
            switch (op)
            {
                case '+': return x + y;
                case '-': return x - y;
                case '*': return x * y;
                default: return -1;
            }
        }
    }
}
