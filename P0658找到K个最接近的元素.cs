using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 658. 找到 K 个最接近的元素 https://leetcode-cn.com/problems/find-k-closest-elements/
给定一个排序好的数组 arr ，两个整数 k 和 x ，从数组中找到最靠近 x（两数之差最小）的 k 个数。返回的结果必须要是按升序排好的。

整数 a 比整数 b 更接近 x 需要满足：

|a - x| < |b - x| 或者
|a - x| == |b - x| 且 a < b
 
     * */
    class P0658找到_K_个最接近的元素
    {
        int _x;
        int[] distances;
        int[] _arr;

        int DisComp(int i, int j)
        {
            int di = distances[i], dj = distances[j];
            if (di < 0) di = distances[i] = Math.Abs(_arr[i] - _x);
            if (dj < 0) dj = distances[j] = Math.Abs(_arr[j] - _x);
            return di == dj ? i - j : di - dj;
        }

        public IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            _x = x;
            _arr = arr;
            distances = new int[arr.Length];
            for (int i = 0; i < distances.Length; distances[i++] = -1) ;
            int start = arr.Length - k, end = arr.Length - 1;
            while (start > 0)
            {
                if (DisComp(start - 1, end) < 0)
                {
                    start--;
                    end--;
                }
                else
                    break;
            }
            return (from i in Enumerable.Range(start, k)
                    select arr[i]).ToList();
        }
    }
}
