using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     * 51 & 52 N皇后（经典回溯）
     * */
    class P0051N皇后
    {
        IList<IList<string>> _ans;
        HashSet<int> _cols, _fxs, _rxs;
        int N;

        void Output(int[] qs)
        {
            IList<string> r = new List<string>();
            foreach (var col in qs)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < col; ++i) sb.Append('.');
                sb.Append('Q');
                for (int i = col + 1; i < N; ++i) sb.Append('.');
                r.Add(sb.ToString());
            }
            _ans.Add(r);
        }

        void Rec(int row, int[] qs)
        {
            if (row >= N)
            {
                Output(qs);
                return;
            }

            for (int col = 0; col < N; ++col)
            {
                int kf = row - col, kr = row + col;
                if (!_cols.Contains(col) && !_fxs.Contains(kf) && !_rxs.Contains(kr))
                {
                    qs[row] = col;
                    _cols.Add(col);
                    _fxs.Add(kf);
                    _rxs.Add(kr);
                    Rec(row + 1, qs);
                    _cols.Remove(col);
                    _fxs.Remove(kf);
                    _rxs.Remove(kr);
                }
            }
        }

        //// 52
        //public int TotalNQueens(int n)
        //{
        //    N = n;
        //    _cols = new HashSet<int>();
        //    _fxs = new HashSet<int>();
        //    _rxs = new HashSet<int>();
        //    _count = 0;
        //    Rec(0);
        //    return _count;
        //}

        // 51
        public IList<IList<string>> SolveNQueens(int n)
        {
            N = n;
            _cols = new HashSet<int>();
            _fxs = new HashSet<int>();
            _rxs = new HashSet<int>();
            _ans = new List<IList<string>>();
            int[] qs = new int[n];
            Rec(0, qs);
            return _ans;
        }
    }
}
