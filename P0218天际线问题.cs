using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 218. 天际线问题
     * 城市的天际线是从远处观看该城市中所有建筑物形成的轮廓的外部轮廓。给你所有建筑物的位置和高度，请返回由这些建筑物形成的 天际线 。
     * */
    class P0218天际线问题
    {
        public IList<IList<int>> GetSkyline(int[][] buildings)
        {
            Heap<int[]> pq = new Heap<int[]>((a, b) => b[1] < a[1]);
            List<int> boundaries = new List<int>();
            foreach (int[] building in buildings)
            {
                boundaries.Add(building[0]);
                boundaries.Add(building[1]);
            }
            boundaries.Sort();
            
            IList<IList<int>> ret = new List<IList<int>>();
            int n = buildings.Length, idx = 0;
            foreach (int boundary in boundaries)
            {
                while (idx < n && buildings[idx][0] <= boundary)
                {
                    pq.Push(new int[] { buildings[idx][1], buildings[idx][2] });
                    idx++;
                }
                while (pq.Count > 0 && pq.Head[0] <= boundary)
                {
                    pq.Pop();
                }

                int maxn = pq.Count == 0 ? 0 : pq.Head[1];
                if (ret.Count == 0 || maxn != ret[ret.Count - 1][1])
                {
                    ret.Add(new List<int>() { boundary, maxn });
                }
            }
            return ret;
        }
    }
}
