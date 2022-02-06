using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0552学生出勤记录II
    {
        public int CheckRecord(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 3;
            if (n == 2) return 8;
            if (n == 3) return 19;
            int dp = 0, dpa = 0, dp1 = 19, dp1a = 7, dp2 = 8, dp2a = 4, dp3 = 3, dp3a = 2;
            for (int i = 3; i < n; ++i)
            {
                dp = dp1.Add(dp1a).Add(dp2).Add(dp2a).Add(dp3).Add(dp3a);
                dpa = dp1a.Add(dp2a).Add(dp3a);
                dp3 = dp2; dp2 = dp1; dp1 = dp;
                dp3a = dp2a; dp2a = dp1a; dp1a = dpa;
            }
            return dp;
        }
    }
}
