using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 并查集 UnionFind
    internal class P0721账户合并
    {
        // official
        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts) 
        {
            Dictionary<string, int> emailToIndex = new();
            Dictionary<string, string> emailToName = new();
            int emailsCount = 0;
            foreach (var account in accounts)
            {
                var name = account[0];
                int size = account.Count;
                for (int i = 1; i < size; i++)
                {
                    var email = account[i];
                    if (!emailToIndex.ContainsKey(email))
                    {
                        emailToIndex.Add(email, emailsCount++);
                        emailToName.Add(email, name);
                    }
                }
            }
            var uf = new UnionFind(emailsCount);
            foreach (var account in accounts)
            {
                var firstEmail = account[1];
                int firstIndex = emailToIndex[firstEmail];
                int size = account.Count;
                for (int i = 2; i < size; i++)
                {
                    var nextEmail = account[i];
                    int nextIndex = emailToIndex[nextEmail];
                    uf.Union(firstIndex, nextIndex);
                }
            }
            Dictionary<int, IList<string>> indexToEmails = new();
            foreach (string email in emailToIndex.Keys)
            {
                int index = uf.Find(emailToIndex[email]);
                if (!indexToEmails.ContainsKey(index))
                    indexToEmails.Add(index, new List<string>());
                indexToEmails[index].Add(email);
            }
            IList<IList<string>> merged = new List<IList<string>>();
            foreach (var emails in indexToEmails.Values)
            {
                List<string> acct = new();
                acct.Add(emailToName[emails.First()]);
                // 遗留问题：这里加了排序但是最终答案还是由于排序错误判了WA，不知道为什么
                List<string> emailList = new(emails);
                emailList.Sort();
                acct.AddRange(emailList);

                merged.Add(acct);
            }
            return merged;
        }
    }
}
