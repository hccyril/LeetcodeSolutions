using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class WC287
    {
        // A
        public int ConvertTime(string current, string correct)
        {
            string[] sp1 = current.Split(':');
            int m1 = int.Parse(sp1[0]) * 60 + int.Parse(sp1[1]);
            string[] sp2 = correct.Split(':');
            int m2 = int.Parse(sp2[0]) * 60 + int.Parse(sp2[1]);
            int diff = m2 - m1;
            int cnt = 0;
            while (diff >= 60)
            {
                diff -= 60;
                cnt++;
            }
            while (diff >= 15)
            {
                diff -= 15;
                cnt++;
            }
            while (diff >= 5)
            {
                diff -= 5;
                cnt++;
            }
            cnt += diff;
            return cnt;
        }

        // B
        class WinStruct
        {
            public int win, lose;
        }
        public IList<IList<int>> FindWinners(int[][] matches)
        {
            Dictionary<int, WinStruct> dic = new();
            foreach (var match in matches)
            {
                int win = match[0], lose = match[1];
                if (!dic.ContainsKey(win)) dic[win] = new();
                if (!dic.ContainsKey(lose)) dic[lose] = new();
                dic[win].win++;
                dic[lose].lose++;
            }
            IList<IList<int>> ans = new List<IList<int>>();
            ans.Add(dic.Where(kv => kv.Value.lose == 0).Select(kv => kv.Key).OrderBy(t => t).ToList());
            ans.Add(dic.Where(kv => kv.Value.lose == 1).Select(kv => kv.Key).OrderBy(t => t).ToList());
            return ans;
        }

        // C
        //[3223352,5458872,1042782,1495999,4705919,4841281,3952468,6322310,3123582,3078264,5890581,2482281,1899305]
        //32898815
        public int MaximumCandies(int[] candies, long k)
        {
            long[] arr = candies.Select(t => (long)t).ToArray();
            long sum = arr.Sum();
            if (sum < k) return 0;
            Array.Sort(arr);
            long l = 1L, r = arr.Last();
            while (l < r)
            {
                long mid = (l + r + 1L) / 2;
                if (Check(arr, mid, k)) l = mid;
                else r = mid - 1;
            }
            return (int)l;
        }

        private bool Check(long[] arr, long m, long k)
        {
            return arr.Select(a => a / m).Sum() >= k;
            // 想多了
            //long n = 0L;
            //for (long l = m; l <= arr.Last(); l += m)
            //{
            //    int i = LowerBound(arr, l);
            //    if (i < arr.Length)
            //    {
            //        n += arr.Length - i;
            //        if (n >= k) return true;
            //    }
            //    else break;
            //}
            //return false;
        }
        //private int LowerBound(long[] arr, long l)
        //{
        //    int i = Array.BinarySearch(arr, l);
        //    if (i < 0) return ~i;
        //    while (i > 0 && arr[i - 1] == l) --i;
        //    return i;
        //}
    }

    // D
    public class Encrypter
    {
        Dictionary<string, int> cnt = new();
        Dictionary<char, string> dic = new();
        Dictionary<string, string> wordDic = new();
        public Encrypter(char[] keys, string[] values, string[] dictionary)
        {
            for (int i = 0; i < keys.Length; i++)
                dic[keys[i]] = values[i];
            foreach (var word in dictionary)
            {
                string encry = string.Join("", word.Select(c => dic[c]));
                wordDic[word] = encry;
                if (!cnt.ContainsKey(encry)) cnt[encry] = 0;
                cnt[encry]++;
            }
        }

        public string Encrypt(string word1)
        {
            return string.Join("", word1.Select(c => dic[c]));
        }

        public int Decrypt(string word2)
        {
            if (cnt.ContainsKey(word2)) return cnt[word2];
            return 0;
        }
    }
}
