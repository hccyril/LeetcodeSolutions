using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P2151基于陈述统计最多好人数
    {
        class PersonStruct
        {
            public int good = 0;
            public int bad = 0;
        }
        PersonStruct[] plist;
        public int MaximumGood(int[][] statements)
        {
            plist = new PersonStruct[statements.Length];
            for (int i = 0; i < plist.Length; ++i) plist[i] = new PersonStruct();
            for (int i = 0; i < statements.Length; ++i)
                for (int j = 0; j < statements[i].Length; ++j)
                    if (statements[i][j] < 2)
                    {
                        if (statements[i][j] == 0)
                            plist[i].bad |= (1 << j);
                        else
                            plist[i].good |= (1 << j);
                    }
            int ans = 0;
            int max = (1 << statements.Length);
            for (int map = 1; map < max; ++map)
                if (Test(statements, map))
                    ans = Math.Max(ans, CountOne(map));
            return ans;
        }

        private int CountOne(int map)
        {
            return map == 0 ? 0 : 1 + CountOne(map & (map - 1));
            //return map == 0 ? 0 : 1 + (map & (map - 1)); // 啊~~~~~！！！！
        }

        private bool Test(int[][] statements, int map)
        {
            for (int i = 0; i < statements.Length; ++i)
                if ((map & (1 << i)) != 0)
                    if ((map & plist[i].good) != plist[i].good ||
                        (map & plist[i].bad) != 0)
                        return false;
            return true;
        }

        //[TestMethod]
        //public void TestMethodA()
        //{
        //    int[][] st = new int[][] { new int[] { 2, 1, 2 }, new int[] { 1, 2, 2 }, new int[] { 2, 0, 2 } };
        //    int ans = MaximumGood(st);
        //    Assert.AreEqual(2, ans);
        //}

    }
}
