using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     * 天啊这题怎么回事啊做了一下午都做不出来！！！
     * 2021/09/11 15:00-17:11(now)
     * */
    class P0224基本计算器
    {
        class CalcStruct
        {
            public int val;
            public char op;
            public bool hasOperator => op == '+' || op == '-';
            public bool hasNoOperator => op == ' ';
            public CalcStruct(char op = ' ') => this.op = op;
        }
        char Merge(Stack<CalcStruct> stk, char op = ' ')
        {
            if (op == '+' || op == '-')
            {
                if (stk.Count > 1 && stk.Peek().hasNoOperator)
                {
                    var cs = stk.Pop();
                    if (stk.Peek().hasOperator) // e.g. 1+2 + 
                    {
                        if (stk.Peek().op == '-')
                            stk.Peek().val -= cs.val;
                        else
                            stk.Peek().val += cs.val;
                    }
                    else // e.g. (1 +
                    {
                        stk.Push(cs);
                    }
                }
                return stk.Peek().op = op;
            }
            else // just merge with upper
            {
                if (stk.Count > 1)
                {
                    var cs = stk.Pop();
                    if (stk.Peek().op == '-')
                        stk.Peek().val -= cs.val;
                    else
                        stk.Peek().val += cs.val;
                }
                char ret = stk.Peek().op;
                stk.Peek().op = op;
                return ret;
            }
        }
        public int Calculate(string s)
        {
            Stack<CalcStruct> stk = new Stack<CalcStruct>();
            stk.Push(new CalcStruct());
            foreach (var c in s)
            {
                switch (c)
                {
                    case ' ': continue;
                    case '+':
                    case '-':
                        Merge(stk, c);
                        break;
                    case '(':
                        stk.Push(new CalcStruct('('));
                        stk.Push(new CalcStruct());
                        break;
                    case ')':
                        char ret = ' ';
                        while (ret != '(') ret = Merge(stk);
                        break;
                    default: // 0 - 9
                        var cs = stk.Peek();
                        if (cs.hasOperator)
                            stk.Push(cs = new CalcStruct());
                        cs.val = cs.val * 10 + (c - '0');
                        break;
                }

            }
            while (stk.Count > 1)
            {
                Merge(stk);
            }
            return stk.Peek().val;
        }

        internal static void Run()
        {
            //string s = "  -3 + 4-5+10"; // 6?
            string s = "(7)-(0)+(4)";
                //"(1+(4+5+2)-3)+(6+8)";
                //" 2-1 + 2 ";
                //"1 + 1";
            Console.WriteLine(new P0224基本计算器().Calculate(s));
        }
    }
}
