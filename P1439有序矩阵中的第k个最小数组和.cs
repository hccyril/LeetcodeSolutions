using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/5/28 Daily
    // 这题验证了int[]不能作为Dictionary的Key
    internal class P1439有序矩阵中的第k个最小数组和
    {
        public int KthSmallest(int[][] mat, int k)
        {
            int m = mat.Length, n = mat[0].Length, i = 0;
            //Dictionary<int[], int> dic = new();
            HashSet<string> hs = new();
            List<int[]> li = new();
            IEnumerable<(int id, int sum)> Next(int[] a)
            {
                for (int i = 0; i < m; ++i)
                    if (a[i] < n - 1)
                    {
                        var b = Enumerable.Range(0, m).Select(j => j == i ? a[j] + 1 : a[j]).ToArray();
                        //if (!dic.ContainsKey(b))
                        if (hs.Add(string.Join(",", b)))
                        {
                            li.Add(b);
                            int id = li.Count - 1,
                                sum = Enumerable.Range(0, m).Select(r => mat[r][b[r]]).Sum();
                            yield return (id, sum);
                        }
                    }
            }
            SHeap<int, int> sort = new((a, b) => a > b),
                hp = new((a, b) => a < b);
            int[] a = new int[m];
            li.Add(a);
            hs.Add(string.Join(",", a)); //dic[a] = 0;
            int sm = mat.Select(r => r[0]).Sum();
            sort.Add(0, sm);
            hp.Add(0, sm);
            for (int iter = 0; iter < k; ++iter)
            {
                (i, sm) = hp.Pop();
                if (iter == k - 1) return sm;
                a = li[i];
                // DEBUG
                Console.WriteLine("[{0}] => {1}",
                    string.Join(" ", Enumerable.Range(0, m).Select(x => mat[x][a[x]])),
                    sm);
                foreach ((int j, int sn) in Next(a))
                {
                    if (sort.Count < k || sn < sort.HeadValue)
                    {
                        if (sort.Count >= k) sort.Pop();
                        sort.Add(j, sn);
                        hp.Add(j, sn);
                    }
                }
            }
            return -1;
        }

        internal static void Run()
        {
            var sln = new P1439有序矩阵中的第k个最小数组和();
            var input = "[[1,10,10],[1,4,5],[2,3,6]]".ToTestInput<int[][]>();
            int k = 7;
            int ans = sln.KthSmallest(input, k);
            Console.WriteLine("ans=" + ans);
        }
    }
}
