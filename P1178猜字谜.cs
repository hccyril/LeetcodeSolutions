using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // LINQ进化版本
    class P1178猜字谜
    {
#if !OK_BUT_TIME_LIMIT_EXCEED // 这个版本4.7s，比原始版本慢了1s左右
        class Puzzle
        {
            public static Dictionary<string, Puzzle> Dict { get; set; }
            public int Index { get; private set; }
            public int Map { get; private set; }
            public int Count { get; set; }

            public Puzzle(string p)
            {
                Index = 1 << (p[0] - 'a');
                Map = p.Aggregate(0, (m, c) => m |= 1 << (c - 'a'));
                Dict[p] = this;
            }
        }
        public IList<int> FindNumOfValidWords(string[] words, string[] puzzles)
        {
            Puzzle.Dict = new Dictionary<string, Puzzle>();
            var groups = from p in puzzles
                         select new Puzzle(p) into po
                         group po by po.Index into gps
                         select gps;
            groups = groups.ToList();

            foreach (var wm in words.Select(word => word.Aggregate(0, (m, c) => m |= 1 << (c - 'a'))))
                foreach (var pl in groups.Where(t => (t.Key & wm) == t.Key))
                    foreach (var puzzle in pl.Where(p => (p.Map & wm) == wm))
                        puzzle.Count++;

            return puzzles.Select(t => Puzzle.Dict[t].Count).ToList();
        }
#endif
#if ALSO_TLE // 这个版本需要21秒，但是原始版本只需要不到4秒
        class Puzzle
        {
            public static Dictionary<string, Puzzle> Dict { get; set; }
            public int Index { get; private set; }
            public int Map { get; private set; }
            public int Count { get; set; }
            public Puzzle(string p)
            {
                Index = 1 << (p[0] - 'a');
                Map = p.Aggregate(0, (m, c) => m |= 1 << (c - 'a'));
                Dict[p] = this;
            }
        }
        public IList<int> FindNumOfValidWords(string[] words, string[] puzzles)
        {
            Puzzle.Dict = new Dictionary<string, Puzzle>();
            var groups = from p in puzzles
                         select new Puzzle(p) into po
                         group po by po.Index into gp
                         select gp;
            groups = groups.ToList();

            (from word in words
             let wm = word.Aggregate(0, (m, c) => m |= 1 << (c - 'a'))
             from gp in groups
             where (gp.Key & wm) == gp.Key
             from p in gp
             where (p.Map & wm) == wm
             select p)
                    .ToList().ForEach(p => p.Count++);

            return puzzles.Select(t => Puzzle.Dict[t].Count).ToList();
        }
#endif

        internal static void Run()
        {
            /*
            ["aaaa","asas","able","ability","actt","actor","access"]
            ["aboveyz","abrodyz","abslute","absoryz","actresz","gaswxyz"]
            ["apple","pleas","please"]
            ["aelwxyz","aelpxyz","aelpsxy","saelpxy","xaelpsy"]
            */
            var tc = new TestCase1178();
            string[] words = tc.Words;
                //{ "aaaa", "asas", "able", "ability", "actt", "actor", "access" };
            string[] puzzles = tc.Puzzles;
            //{ "aboveyz", "abrodyz", "abslute", "absoryz", "actresz", "gaswxyz" };
            var output = new P1178猜字谜().FindNumOfValidWords(words, puzzles);
            //var output = new Solution().FindNumOfValidWords(words, puzzles);
            Console.WriteLine(string.Join(" ", output));
        }

        // 初代版本：
        public class Solution
        {
            class Puzzle
            {
                public char HeadChar => pw[0];
                public int Index => HeadChar - 'a';
                public string pw;
                public int map;
                public int count;
                public Puzzle(string p)
                {
                    pw = p;
                    map = 0;
                    foreach (var c in p) map |= 1 << (c - 'a');
                }
            }
            class PuzzleList : List<Puzzle>
            {
                public int Index { get; private set; }
                public int Map { get; private set; }
                public PuzzleList(int index) : base()
                {
                    Index = index;
                    Map = 1 << index;
                }
            }
            public IList<int> FindNumOfValidWords(string[] words, string[] puzzles)
            {
                Dictionary<string, Puzzle> pdic = new Dictionary<string, Puzzle>();
                PuzzleList[] pl = new PuzzleList[26];
                foreach (var pw in puzzles)
                {
                    Puzzle pz = new Puzzle(pw);
                    int index = pz.Index;
                    if (pl[index] == null) pl[index] = new PuzzleList(index);
                    pl[index].Add(pz);
                    pdic[pw] = pz;
                }
                foreach (var word in words)
                {
                    int wm = new Puzzle(word).map;
                    foreach (var puzzleList in pl.Where(t => t != null && (t.Map & wm) == t.Map))
                        foreach (var puzzle in puzzleList)
                            if ((wm & puzzle.map) == wm)
                                puzzle.count++;
                }

                return puzzles.Select(t => pdic[t].count).ToList();
            }
        }
    }
}
