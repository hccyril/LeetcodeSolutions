using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/3/2, 第57双周赛题C
    // 差分数组
    internal class P1943描述绘画结果
    {
        // ver2 - 输入的color一定是distinct的，所以没必要存每个颜色
        public IList<IList<long>> SplitPainting_2(int[][] segments)
        {
            Dictionary<int, int> map = new();
            IList<IList<long>> ansList = new List<IList<long>>();
            foreach (int[] seg in segments)
            {
                int start = seg[0], end = seg[1], color = seg[2];
                if (map.ContainsKey(start)) map[start] = map[start] + color;
                else map[start] = color;
                if (map.ContainsKey(end)) map[end] = map[end] - color;
                else map[end] = -color;
            }
            int[] arr = new int[map.Count];
            int i = 0;
            foreach (int key in map.Keys)
            {
                arr[i++] = key;
            }
            Array.Sort(arr);
            long sum = 0L; i = 0;
            foreach (int key in arr)
            {
                if (sum > 0)
                {
                    List<long> list = new();
                    list.Add((long)i);
                    list.Add((long)key);
                    list.Add(sum);
                    ansList.Add(list);
                }
                i = key;
                sum += map[i];
            }
            return ansList;
        }

        // 比赛时代码，AC
        public IList<IList<long>> SplitPainting_1(int[][] segments)
        {
            SortedDictionary<long, SortedSet<long>> sort = new();
            foreach (var seg in segments)
            {
                int start = seg[0], end = seg[1], color = seg[2];
                if (!sort.ContainsKey(start)) sort[start] = new();
                if (!sort.ContainsKey(end)) sort[end] = new();
                if (sort[start].Contains(-color))
                {
                    sort[start].Remove(-color);
                    if (sort[start].Count == 0) sort.Remove(start);
                }
                else
                {
                    sort[start].Add(color);
                }
                if (sort[end].Contains(color))
                {
                    sort[end].Remove(color);
                    if (sort[end].Count == 0) sort.Remove(end);
                }
                else
                {
                    sort[end].Add(-color);
                }
            }
            IList<IList<long>> ansList = new List<IList<long>>();
            long st = 0L, sum = 0L;
            foreach ((long t, SortedSet<long> colors) in sort)
            {
                if (st > 0L)
                {
                    ansList.Add(new List<long>() { st, t, sum });
                }
                st = t;
                sum += colors.Sum();
            }
            return ansList;
        }

        internal static void Run()
        {
            var input = Common.ReadInput<int[][]>(1943);
            var sln = new P1943描述绘画结果();
            var ans1 = sln.SplitPainting_1(input);
            var ans2 = sln.SplitPainting_2(input);
            if (ans1.Count != ans1.Count)
                Console.WriteLine("Count: {0} <> {1}", ans1.Count, ans2.Count);
            else
            {
                for (int i = 0; i < ans1.Count; ++i)
                {
                    if (ans1[i][0] != ans2[i][0] || ans1[i][1] != ans2[i][1] || ans2[i][2] != ans1[i][2])
                    {
                        Console.WriteLine("i: {0} \n ans1: {1} \n ans2: {2}",
                            i, string.Join(",", ans1[i]), string.Join(",", ans2[i]));
                        break;
                    }
                }
            }
            Console.WriteLine("done");
        }

    }
}
