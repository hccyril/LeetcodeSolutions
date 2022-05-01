using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P0591标签验证器
    {
        Stack<string> stack = new();
        bool contains_tag = false;
        bool IsValidTagName(string s, bool ending)
        {
            if (s.Length < 1 || s.Length > 9)
                return false;
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsUpper(s[i]))
                    return false;
            }
            if (ending)
            {
                if (stack.Any() && stack.Peek().Equals(s))
                    stack.Pop();
                else
                    return false;
            }
            else
            {
                contains_tag = true;
                stack.Push(s);
            }
            return true;
        }
        bool IsValidCdata(string s) => s.StartsWith("[CDATA[");
        public bool IsValid(string code)
        {
            stack = new();
            contains_tag = false;
            if (code[0] != '<' || code[code.Length - 1] != '>')
                return false;
            for (int i = 0; i < code.Length; i++)
            {
                bool ending = false;
                int closeindex;
                if (!stack.Any() && contains_tag)
                    return false;
                if (code[i] == '<')
                {
                    if (stack.Any() && code[i + 1] == '!')
                    {
                        closeindex = code.IndexOf("]]>", i + 1);
                        if (closeindex < 0 || !IsValidCdata(code.Substring(i + 2, closeindex - i - 2)))
                            return false;
                    }
                    else
                    {
                        if (code[i + 1] == '/')
                        {
                            i++;
                            ending = true;
                        }
                        closeindex = code.IndexOf('>', i + 1);
                        if (closeindex < 0 || !IsValidTagName(code.Substring(i + 1, closeindex - i - 1), ending))
                            return false;
                    }
                    i = closeindex;
                }
            }
            return !stack.Any() && contains_tag;
        }

        internal static void Run()
        {
            string s1 = "<DIV>This is the first line <![CDATA[<div>]]></DIV>", // true
                s2 = "<DIV>>>  ![cdata[]] <![CDATA[<div>]>]]>]]>>]</DIV>", // true
                s3 = "<A>  <B> </A>   </B>"; // false
            var sln = new P0591标签验证器();
            Console.WriteLine(sln.IsValid(s1));
            Console.WriteLine(sln.IsValid(s2));
            Console.WriteLine(sln.IsValid(s3));
        }
    }
}
