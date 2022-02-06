using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{

    class P0043字符串相乘
    {
        class BigInt : List<int>
        {
            public void AddNum(int n, int i)
            {
                while (Count <= i) Add(0);
                this[i] += n;
                int j = this[i] / 10;
                this[i] %= 10;
                if (j > 0) AddNum(j, i + 1);
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = Count - 1; i >= 0; --i)
                    sb.Append(this[i]);
                string st = sb.ToString().TrimStart('0');
                return string.IsNullOrEmpty(st) ? "0" : st;
            }
        }

        /*
         * 415. 字符串相加
         */
        public string AddStrings(string num1, string num2)
        {
            var ans = new BigInt();
            for (int i = 0; i < num1.Length; ++i)
                ans.AddNum(num1[num1.Length - i - 1] - '0', i);
            for (int i = 0; i < num2.Length; ++i)
                ans.AddNum(num2[num2.Length - i - 1] - '0', i);
            return ans.ToString();
        }

        public string Multiply(string num1, string num2)
        {
            var ans = new BigInt();
            for (int i1 = num1.Length - 1; i1 >= 0; --i1)
                for (int i2 = num2.Length - 1; i2 >= 0; --i2)
                    ans.AddNum((num1[i1] - '0') * (num2[i2] - '0'), num1.Length - i1 + num2.Length - i2 - 2);
            return ans.ToString();
        }


    }
}
