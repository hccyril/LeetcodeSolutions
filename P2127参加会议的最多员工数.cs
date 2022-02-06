using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛题
    internal class P2127参加会议的最多员工数
    {
        Tree[] trees;
        int[] gps;
        int[] favorite;
        class Tree
        {
            public int i;
            private List<int> cldn;
            public void Add(int i)
            {
                if (cldn == null) cldn = new();
                cldn.Add(i);
            }
            public IEnumerable<int> Children => cldn == null ? Array.Empty<int>() : cldn;
            //public IEnumerable<Tree> Children => cldn == null ? Array.Empty<Tree>() : cldn.Select(i => trees[i]);
        }
        int MaxChain(int i)
        {
            int max = 0;
            foreach (var c in trees[i].Children)
                if (gps[c] >= 0)
                {
                    gps[c] = -1;
                    max = Math.Max(max, MaxChain(c));
                }
            return max + 1;
        }
        public int MaximumInvitations(int[] favorite)
        {
            int chain = 0, circle = 0;
            this.favorite = favorite;
            trees = new Tree[favorite.Length];
            for (int i = 0; i < favorite.Length; i++) trees[i] = new Tree() { i = i };
            for (int i = 0; i < favorite.Length; i++) trees[favorite[i]].Add(i);
            gps = new int[favorite.Length];
            foreach (int i in Enumerable.Range(0, favorite.Length))
            {
                // build up the chain or circle
                int it = i, count = 0;
                while (gps[it] == 0)
                {
                    gps[it] = ++count;
                    if (favorite[favorite[it]] == it)
                    {
                        int a = it, b = favorite[it];
                        gps[a] = gps[b] = -1;
                        chain += MaxChain(a) + MaxChain(b);
                        count = -1;
                        break;
                    }
                    it = favorite[it];
                }
                if (gps[it] > 0)
                    circle = Math.Max(circle, count - gps[it] + 1);
                if (count > 0)
                {
                    for (int k = i; gps[k] > 0; k = favorite[k]) gps[k] = -1;
                }
                // WRONG! 还是要细心点，要是在比赛中就麻烦了
                //if (count > 0)
                //{
                //    gps[it] = -1;
                //    for (int k = i; k != it; k = favorite[k]) gps[k] = -1;
                //}
            }
            return Math.Max(chain, circle);
        }

        internal static void Run()
        {
            // correct answer: 11
            int[] input = { 51, 395, 341, 380, 231, 423, 112, 382, 233, 211, 128, 93, 116, 123, 131, 44, 127, 305, 281, 300, 42, 252, 373, 232, 240, 280, 1, 426, 282, 315, 250, 194, 321, 351, 273, 167, 423, 428, 77, 101, 59, 161, 263, 84, 303, 398, 176, 285, 63, 319, 181, 374, 32, 249, 237, 32, 112, 308, 424, 38, 147, 412, 227, 197, 340, 232, 414, 268, 151, 252, 428, 136, 291, 425, 35, 35, 260, 141, 47, 362, 25, 287, 271, 37, 154, 302, 137, 304, 252, 333, 261, 119, 11, 120, 138, 181, 239, 46, 245, 215, 227, 202, 235, 33, 233, 77, 27, 429, 334, 359, 198, 405, 19, 363, 150, 23, 105, 300, 195, 83, 63, 90, 61, 361, 133, 405, 14, 250, 290, 48, 296, 429, 224, 374, 405, 50, 255, 266, 124, 146, 134, 286, 1, 347, 413, 373, 60, 103, 42, 392, 139, 283, 167, 400, 96, 152, 239, 291, 389, 312, 20, 77, 426, 277, 128, 354, 138, 219, 352, 213, 230, 32, 187, 188, 325, 28, 148, 161, 165, 71, 325, 297, 324, 328, 328, 339, 58, 374, 324, 148, 70, 417, 120, 2, 411, 298, 321, 327, 116, 185, 229, 409, 368, 335, 386, 316, 161, 113, 96, 112, 328, 212, 82, 166, 57, 306, 188, 403, 230, 19, 427, 272, 380, 46, 48, 212, 93, 304, 252, 213, 181, 328, 388, 23, 327, 254, 378, 42, 122, 313, 168, 172, 199, 5, 389, 308, 302, 325, 273, 115, 85, 212, 218, 331, 389, 390, 75, 33, 207, 281, 231, 410, 24, 35, 262, 11, 122, 409, 91, 339, 49, 397, 146, 382, 341, 107, 239, 113, 32, 135, 250, 344, 129, 9, 278, 269, 140, 78, 366, 426, 2, 173, 386, 162, 177, 288, 42, 51, 252, 165, 41, 386, 95, 39, 182, 22, 183, 175, 326, 326, 16, 113, 249, 386, 357, 95, 18, 401, 215, 115, 123, 213, 310, 5, 276, 213, 320, 230, 334, 133, 148, 425, 15, 55, 79, 275, 83, 239, 291, 211, 417, 90, 291, 67, 362, 304, 251, 177, 361, 36, 149, 141, 119, 349, 31, 84, 289, 415, 251, 192, 56, 421, 306, 156, 306, 143, 379, 226, 337, 282, 426, 210, 167, 337, 335, 298, 284, 65, 178, 112, 66, 103, 365, 251, 318, 309, 12, 151, 252, 140, 118, 114, 193, 331, 346, 271, 165, 121, 15, 309, 96, 143, 98, 13, 314, 213, 148, 194, 411, 124, 64, 301, 281, 365, 361, 100, 242, 212, 254, 331, 410, 408, 149, 229, 189, 271, 166, 6, 151, 252 };
            int output = new P2127参加会议的最多员工数().MaximumInvitations(input);
            Console.WriteLine(output);
        }
    }
}
