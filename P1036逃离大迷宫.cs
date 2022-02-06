using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, advanced BFS // 2022/01/11
    // 使用了DFS，由题目可知最多200个障碍，能圈住的最大面积为20000， 因此能走超过20000步即可推断没有被障碍围住 - 2022/1/12
    internal class P1036逃离大迷宫
    {
        HashSet<long> visited = new();
        HashSet<long> blocks = new();
        long Encode(int[] p) => Encode(p[0], p[1]);
        long Encode(int x, int y) => (long)x << 30 | (long)y;
        IEnumerable<(int x, int y)> Moves(int x0, int y0)
        {
            if (x0 > 0) yield return (x0 - 1, y0);
            if (x0 < 999999) yield return (x0 + 1, y0);
            if (y0 > 0) yield return (x0, y0 - 1);
            if (y0 < 999999) yield return (x0, y0 + 1);
        }
        // return: 0 failed, 1 succeed, 2 escaped (exceeded 20000 steps)
        int Dfs(int x, int y, int targetX, int targetY)
        {
            foreach ((int x1, int y1) in Moves(x, y))
            {
                if (targetX == x1 && targetY == y1) return 1;
                long key = Encode(x1, y1);
                if (!blocks.Contains(key) && !visited.Contains(key))
                {
                    visited.Add(key);
                    if (visited.Count > 20000) return 2;
                    int ret = Dfs(x1, y1, targetX, targetY);
                    if (ret > 0) return ret;
                }
            }
            return 0;
        }
        public bool IsEscapePossible(int[][] blocked, int[] source, int[] target)
        {
            foreach (var p in blocked) blocks.Add(Encode(p));
            visited.Add(Encode(source));
            int ret = Dfs(source[0], source[1], target[0], target[1]);
            if (ret == 2)
            {
                visited.Clear();
                visited.Add(Encode(target));
                ret = Dfs(target[0], target[1], source[0], source[1]);
            }
            return ret > 0;
        }


        /* official
class Solution {
private:
    static constexpr int BOUNDARY = 1000000;
    static constexpr int dirs[4][2] = {{0, 1}, {0, -1}, {1, 0}, {-1, 0}};
    
public:
    bool isEscapePossible(vector<vector<int>>& blocked, vector<int>& source, vector<int>& target) {
        if (blocked.size() < 2) {
            return true;
        }
        vector<int> rows, columns;
        for (const auto& pos: blocked) {
            rows.push_back(pos[0]);
            columns.push_back(pos[1]);
        }
        rows.push_back(source[0]);
        rows.push_back(target[0]);
        columns.push_back(source[1]);
        columns.push_back(target[1]);
        
        // 离散化
        sort(rows.begin(), rows.end());
        sort(columns.begin(), columns.end());
        rows.erase(unique(rows.begin(), rows.end()), rows.end());
        columns.erase(unique(columns.begin(), columns.end()), columns.end());
        unordered_map<int, int> r_mapping, c_mapping;

        int r_id = (rows[0] == 0 ? 0 : 1);
        r_mapping[rows[0]] = r_id;
        for (int i = 1; i < rows.size(); ++i) {
            r_id += (rows[i] == rows[i - 1] + 1 ? 1 : 2);
            r_mapping[rows[i]] = r_id;
        }
        if (rows.back() != BOUNDARY - 1) {
            ++r_id;
        }

        int c_id = (columns[0] == 0 ? 0 : 1);
        c_mapping[columns[0]] = c_id;
        for (int i = 1; i < columns.size(); ++i) {
            c_id += (columns[i] == columns[i - 1] + 1 ? 1 : 2);
            c_mapping[columns[i]] = c_id;
        }
        if (columns.back() != BOUNDARY - 1) {
            ++c_id;
        }

        vector<vector<int>> grid(r_id + 1, vector<int>(c_id + 1));
        for (const auto& pos: blocked) {
            int x = pos[0], y = pos[1];
            grid[r_mapping[x]][c_mapping[y]] = 1;
        }
        
        int sx = r_mapping[source[0]], sy = c_mapping[source[1]];
        int tx = r_mapping[target[0]], ty = c_mapping[target[1]];

        queue<pair<int, int>> q;
        q.emplace(sx, sy);
        grid[sx][sy] = 1;
        while (!q.empty()) {
            auto [x, y] = q.front();
            q.pop();
            for (int d = 0; d < 4; ++d) {
                int nx = x + dirs[d][0], ny = y + dirs[d][1];
                if (nx >= 0 && nx <= r_id && ny >= 0 && ny <= c_id && grid[nx][ny] != 1) {
                    if (nx == tx && ny == ty) {
                        return true;
                    }
                    q.emplace(nx, ny);
                    grid[nx][ny] = 1;
                }
            }
        }
        return false;
    }
};

作者：LeetCode-Solution
链接：https://leetcode-cn.com/problems/escape-a-large-maze/solution/tao-chi-da-mi-gong-by-leetcode-solution-qxhz/
来源：力扣（LeetCode）
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。
         * */
    }
}
