using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0087扰乱字符串
    {
        int[,,] dp;
        int[] lc = new int[26];
        bool Scan(string s1, string s2, int i1, int i2, int len)
        {
            if (dp[i1, i2, len] != 0) return dp[i1, i2, len] > 0;
            if (len == 1) return (dp[i1, i2, len] = s1[i1] == s2[i2] ? 1 : -1) > 0;

            int[] arr = new int[26];

            for (int cut = 1; cut < len; ++cut) // 枚举每一种切分情况，将s1切成cut和len-cut个字符
            {
                for (int iflag = 0; iflag < 2; ++iflag)// 对于每一种切分，又要枚举是否交换位置
                {
                    bool flag = iflag == 0;
                    Array.Copy(lc, arr, 26);
                    int shift = flag ? 0 : len - cut;
                    for (int i = 0; i < cut; ++i)
                        arr[s1[i1 + i] - 'a']++;
                    bool ok = true;
                    for (int i = 0; i < cut; ++i)
                    {
                        if (--arr[s2[i2 + i + shift] - 'a'] < 0)
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        int shift2 = flag ? cut : 0;
                        if (Scan(s1, s2, i1, i2 + shift, cut) && Scan(s1, s2, i1 + cut, i2 + shift2, len - cut))
                            return (dp[i1, i2, len] = 1) > 0;
                    }
                }
            }
            return (dp[i1, i2, len] = -1) > 0;
        }
        public bool IsScramble(string s1, string s2)
        {
            // 初步检查，确认s1和s2包含完全相同的字符
            for (int i = 0; i < s1.Length; ++i)
                lc[s1[i] - 'a']++;
            for (int i = 0; i < s2.Length; ++i)
                if (--lc[s2[i] - 'a'] < 0)
                    return false;

            // 回溯+缓存(=动态规划)
            dp = new int[s1.Length + 1, s1.Length + 1, s1.Length + 1];
            return Scan(s1, s2, 0, 0, s1.Length);
        }

        public static void Run()
        {
            string s1 = "great", s2 = "rgeat";
            Console.WriteLine(new P0087扰乱字符串().IsScramble(s1, s2));
        }
    }
}
