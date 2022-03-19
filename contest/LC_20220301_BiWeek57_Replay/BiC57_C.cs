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
    public class BiC57_C
    {
        public IList<IList<long>> SplitPainting(int[][] segments)
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

        [TestMethod]
        public void TestMethodC()
        {
            var input = new int[][] { new int[] { 1, 4, 5 }, new int[] { 1, 4, 7 }, new int[] { 4, 7, 1 }, new int[] { 4, 7, 11 } };
            var output = SplitPainting(input);
            Assert.AreEqual(2, output.Count);
        }
    }
}
