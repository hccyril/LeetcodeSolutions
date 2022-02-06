using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, string parser
    internal class P0394字符串解码
    {
        string s;
        int i;
        bool End() => i >= s.Length;
        bool IsDigit() => !End() && char.IsDigit(s[i]);
        bool IsLetter() => !End() && char.IsLetter(s[i]);
        char Read() => End() ? (char)0 : s[i++];
        bool IsEndBracket() => End() || s[i] == ']';
        void ReadEndBracket() => i = i < s.Length && s[i] == ']' ? i + 1 : i;

        // 这个方法的逻辑搞了一上午，吐了。。。
        string Parse()
        {
            StringBuilder pn = new();
            if (IsDigit())
            {
                int n = 0;
                char c;
                while ((c = Read()) != '[') n = n * 10 + (c - '0');
                string p = Parse();
                ReadEndBracket();
                while (--n >= 0) pn.Append(p);
            }
            else
            {
                while (IsLetter()) pn.Append(Read());
            }
            if (!IsEndBracket()) pn.Append(Parse());
            return pn.ToString();
        }
        public string DecodeString(string s)
        {
            this.s = s; i = 0;
            return Parse();
        }


        /* 写了弱智代码，还审错题，整个重写了
        string s;
        StringBuilder printer;
        int i;
        bool End() => i >= s.Length;
        bool IsDigit() => s[i] >= '0' && s[i] <= '9';
        char Read() => End() ? (char)0 : s[i++];
        (int n, string p) Parse()
        {
            int n = 0;
            StringBuilder pn = new();
            if (!IsDigit())
            {
                n = 1;
                while (!End() && !IsDigit()) pn.Append(Read());
                return (n, pn.ToString());
            }
            else
            {
                char c;
                while ((c = Read()) != '[') n = n * 10 + (c - '0');
                while ((c = Read()) != ']') pn.Append(c);
                // 我怎么写了这么弱智的代码= =|||
                //for (char c = Read(); c != '['; n = n * 10 + (c - '0')) ;
                //for (char c = Read(); c != ']'; pn.Append(c)) ;
                return (n, pn.ToString());
            }
        }
        void Print((int n, string p) args)
        {
            while (--args.n >= 0) printer.Append(args.p);
        }
        public string DecodeString(string s)
        {
            this.s = s; i = 0; printer = new();
            while (!End()) Print(Parse());
            return printer.ToString();
        }
        */

        internal static void Run()
        {
            string s = "3[z]2[2[y]pq4[2[jk]e1[f]]]ef";
                //"3[a]2[bc]";
            Console.WriteLine(new P0394字符串解码().DecodeString(s));
        }
    }
}
