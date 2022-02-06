using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 有一堆石头，用整数数组 stones 表示。其中 stones[i] 表示第 i 块石头的重量。

每一回合，从中选出任意两块石头，然后将它们一起粉碎。假设石头的重量分别为 x 和 y，且 x <= y。那么粉碎的可能结果如下：

如果 x == y，那么两块石头都会被完全粉碎；
如果 x != y，那么重量为 x 的石头将会完全粉碎，而重量为 y 的石头新重量为 y-x。
最后，最多只会剩下一块 石头。返回此石头 最小的可能重量 。如果没有石头剩下，就返回 0。

 

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/last-stone-weight-ii
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P1049最后一块石头的重量II
    {
        /// <summary>
        /// 这题的问题在于没想通，本质就是石头分两堆然后和的差值最小，这样就跟昨天（494）的问题一样了
        /// </summary>
        /// <param name="stones"></param>
        /// <returns></returns>
        public int LastStoneWeightII(int[] stones)
        {
            int sum = 0;
            foreach (var s in stones)
                sum += s;
            int half = sum / 2;

            HashSet<int> hs = new HashSet<int>();
            int[] arr = new int[600]; // 最多1+2+...+length，所以30*30/2=450
            int i = 0;
            foreach (var num in stones)
            {
                i = 0;
                if (num <= half && !hs.Contains(num))
                    arr[i++] = num; // hs.Add(num);
                foreach (var n in hs)
                {
                    int nm = n + num;
                    if (nm <= half && !hs.Contains(nm))
                        arr[i++] = nm;
                }
                for (--i; i >= 0; --i)
                    hs.Add(arr[i]);
            }
            int min = sum;
            foreach (var halfsum in hs)
            {
                int diff = Math.Abs(sum - halfsum - halfsum);
                if (diff < min)
                    min = diff;
            }
            return min;
        }

        /** failed
         * 
        int min = -1;

        void Rec(List<int> list)
        {
            if (list.Count <= 1)
            {
                if (list.Count == 0)
                {
                    min = 0;
                }
                else if (min > list[0])
                {
                    min = list[0];
                }
                return;
            }

            // case 1
            int last = list.Count - 1;
            int a = list[last];
            int b = list[last - 1];
            list.RemoveAt(last);
            list.RemoveAt(last - 1);
            int c = a - b;
            if (c > 0)
            {
                list.Add(c);
                list.Sort();
            }
            // rec 1
            Rec(list); if (min == 0) return;
            // rec 1 - restore
            if (c > 0) list.Remove(c);
            list.Add(a);
            list.Add(b);
            list.Sort();

            // case 2
            if (list.Count > 2)
            {
                a = list[0];
                b = list[1];
                list.RemoveAt(0);
                list.RemoveAt(0);
                c = a + b;
                list.Add(c);
                list.Sort();
                // rec 2
                Rec(list); if (min == 0) return;
                // rec2 - restore
                list.Remove(c);
                list.Add(a);
                list.Add(b);
                list.Sort();
            }
        }

        public int LastStoneWeightII(int[] stones)
        {
            List<int> list = new List<int>(stones);
            list.Sort();
            min = list[list.Count - 1];

            Rec(list);
            return min;



            // ver 1 : greedy, wrong answer for [40,33,31,26,21]
            //while (list.Count > 1)
            //{
            //    int last = list.Count - 1;
            //    int a = list[last];
            //    int b = list[last - 1];
            //    list.RemoveAt(last);
            //    list.RemoveAt(last - 1);
            //    int c = a - b;
            //    if (c > 0)
            //    {
            //        list.Add(c);
            //        list.Sort();
            //    }
            //}
            //return list.Count == 0 ? 0 : list[0];
        } */
    }
}
