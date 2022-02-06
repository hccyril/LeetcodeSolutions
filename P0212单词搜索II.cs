using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /// <summary>
    /// 前缀树
    /// </summary>
    class P0212单词搜索II
    {
        WordTree treeRoot;
        char[][] board;
        HashSet<string> ansList;
        
        int Key(int i, int j) => (i << 6) | j;
        int[][] dirs = { new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { -1, 0 }, new int[] { 0, -1 } };
        IEnumerable<int[]> Directions(int i, int j)
        {
            foreach (var dir in dirs)
            {
                int ii = i + dir[0], jj = j + dir[1];
                if (ii >= 0 && ii < board.Length && jj >= 0 && jj < board[0].Length)
                    yield return dir;
            }
        }
        void Dfs(int i, int j, WordTree tree, HashSet<int> path)
        {
            char c = board[i][j];
            if (!tree.ContainsChar(c)) return;
            tree = tree[c];
            if (tree.Word != null) 
                ansList.Add(tree.Word);
            if (tree.End())
                return;
            path.Add(Key(i, j));
            foreach (var di in Directions(i, j))
            {
                int nexti = i + di[0], nextj = j + di[1];
                if (!path.Contains(Key(nexti, nextj)))
                    Dfs(nexti, nextj, tree, path);
            }
            path.Remove(Key(i, j));
        }
        public IList<string> FindWords(char[][] board, string[] words)
        {
            this.board = board;
            ansList = new HashSet<string>();
            treeRoot = new WordTree();
            foreach (var word in words)
                treeRoot.AddWord(word);
            for (int i = 0; i < board.Length; ++i)
                for (int j = 0; j < board[i].Length; ++j)
                {
                    Dfs(i, j, treeRoot, new HashSet<int>());
                }
            return ansList.ToList();
        }
    }
}
