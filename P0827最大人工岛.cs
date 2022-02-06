using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0827最大人工岛
    {
        class Land
        {
            static int cc = 1;
            int color = cc++;
            int Color => Origin.color;
            Land next = null;
            Land Origin => next == null ? this : next.Origin; //next ?? this;
            int area = 0;
            public int Area => Origin.area; 
            Land Merge(Land l2)
            {
                if (!Equal(l2))
                {
                    //if (Origin == l2.Origin)
                    //    throw new ArgumentException("origin");
                    //color = l2.color;
                    l2.Origin.area += Area; // area; // 最后一步卡在这里！！！
                    Origin.next = l2.Origin; // next = l2.Origin;
                }
                return this;
            }
            Land Add1()
            {
                Origin.area++;
                return this;
            }
            public bool Equal(Land l2)
            {
                return Color == l2.Color;
            }
            public static Land Merge(Land l1, Land l2, int p)
            {
                if (p > 0)
                {
                    return (l1 != null && l2 != null ? l1.Merge(l2) :
                        l1 == null && l2 == null ? new Land() :
                        l1 != null ? l1 : l2).Add1();
                }
                return null;
            }
        }

        class LandArea
        {
            public int MaxArea;
            int area;
            IList<Land> list = new List<Land>();
            public LandArea Clear()
            {
                list.Clear();
                Max(area = 1);
                return this;
            }
            public void Max(int area)
            {
                if (area > MaxArea) MaxArea = area;
            }
            public LandArea AddLand(Land l)
            {
                if (l != null)
                {
                    foreach (var ll in list)
                        if (ll.Equal(l))
                            return this;
                    list.Add(l);
                    Max(area += l.Area);
                }
                return this;
            }
        }

        public int LargestIsland(int[][] grid)
        {
            Land[,] lands = new Land[grid.Length, grid.Length];
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid.Length; ++j)
                {
                    Land landUp = i > 0 ? lands[i - 1, j] : null;
                    Land landLeft = j > 0 ? lands[i, j - 1] : null;
                    lands[i, j] = Land.Merge(landUp, landLeft, grid[i][j]);
//#if DEBUG
//                    if (i > 0 && j == 4)
//                        Console.WriteLine("area " + lands[1, 1].Area);
//#endif
                }
            LandArea landArea = new LandArea();
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid.Length; ++j)
                    if (grid[i][j] == 1)
                    {
                        landArea.Max(lands[i, j].Area);
                    }
                    else
                    {
                        landArea.Clear()
                            .AddLand(i > 0 ? lands[i - 1, j] : null)
                            .AddLand(j > 0 ? lands[i, j - 1] : null)
                            .AddLand(j < grid.Length - 1 ? lands[i, j + 1] : null)
                            .AddLand(i < grid.Length - 1 ? lands[i + 1, j] : null);
                    }
            return landArea.MaxArea;
        }

        public static void Run()
        {
            //var input = new int[][] { new int[] { 1, 0, 1, 0, 1 }, new int[] { 0, 1, 1, 0, 1 }, new int[] { 1, 1, 1, 0, 0 }, new int[] { 1, 0, 1, 1, 1 }, new int[] { 0, 0, 1, 1, 0 } };
            var input = new int[][] { 
                new int[] { 0, 1, 0, 0, 1, 0, 0, 0 }, 
                new int[] { 1, 1, 0, 1, 0, 1, 1, 0 }, 
                new int[] { 1, 1, 1, 0, 0, 1, 1, 1 }, 
                new int[] { 1, 0, 0, 1, 1, 0, 1, 0 }, 
                new int[] { 0, 0, 1, 1, 1, 1, 0, 1 }, 
                new int[] { 0, 0, 1, 1, 1, 0, 1, 0 }, 
                new int[] { 0, 0, 1, 0, 1, 0, 0, 0 }, 
                new int[] { 0, 0, 0, 1, 1, 1, 1, 0 } };
            Console.WriteLine(new P0827最大人工岛().LargestIsland(input));
        }
    }
}
