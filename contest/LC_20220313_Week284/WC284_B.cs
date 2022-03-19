using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ProblemB
    {
        public int DigArtifacts(int n, int[][] artifacts, int[][] dig)
        {
            int[,] map = new int[n, n];
            List<int> areas = new();
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    map[i, j] = -1;
            for (int i = 0; i < artifacts.Length; ++i)
            {
                int i1 = artifacts[i][0], j1 = artifacts[i][1], i2 = artifacts[i][2], j2 = artifacts[i][3];
                areas.Add((i2 - i1 + 1) * (j2 - j1 + 1));
                for (int ii = i1; ii <= i2; ++ii)
                    for (int jj = j1; jj <= j2; ++jj)
                        map[ii, jj] = i;
            }
            foreach (var d in dig)
            {
                int di = map[d[0], d[1]];
                if (di >= 0)
                    areas[di]--;
            }
            return areas.Count(t => t == 0);
        }

        // ver1 执行出错
        // Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
        // Line 3: Solution.DigArtifacts(Int32 n, Int32[][] artifacts, Int32[][] dig) in Solution.cs
        // 最后执行的输入：
        // 5
        // [[3,1,4,1],[1,1,2,2],[1,0,2,0],[4,3,4,4],[0,3,1,4],[2,3,3,4]]
        // [[0,0],[2,1],[2,0],[2,3],[4,3],[2,4],[4,1],[0,2],[4,0],[3,1],[1,2],[1,3],[3,2]]
        public int DigArtifacts_WRONG(int n, int[][] artifacts, int[][] dig)
        {
            int[,] map = new int[n, n];
            List<int> areas = new();
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    map[i, j] = -1;
            for (int i = 0; i < artifacts.Length; ++i)
            {
                int i1 = artifacts[i][0], j1 = artifacts[i][1], i2 = artifacts[i][2], j2 = artifacts[i][3];
                areas.Add((i2 - i1 + 1) * (j2 - j1 + 1));
                for (int ii = i1; ii <= i2; ++ii)
                    for (int jj = j1; jj <= j2; ++jj)
                        map[ii, jj] = i;
            }
            foreach (var d in dig)
            {
                int di = map[d[0], d[1]];
                areas[di]--; // <-- 在这里漏了判断di有可能等于-1
            }
            return areas.Count(t => t == 0);
        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
