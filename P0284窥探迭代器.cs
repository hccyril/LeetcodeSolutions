using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 这题没什么，就是IEnumerator的使用
    class P0284窥探迭代器
    {
        // C# IEnumerator interface reference:
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=netframework-4.8

        class PeekingIterator
        {
            IEnumerator<int> it;
            bool hasNext = true;

            // iterators refers to the first element of the array.
            public PeekingIterator(IEnumerator<int> iterator)
                // initialize any member here.
                => it = iterator;

            // Returns the next element in the iteration without advancing the iterator.
            public int Peek() => it.Current;

            // Returns the next element in the iteration and advances the iterator.
            public int Next()
            {
                int current = it.Current;
                if (hasNext) hasNext = it.MoveNext();
                return current;
            }

            // Returns false if the iterator is refering to the end of the array of true otherwise.
            public bool HasNext() => hasNext;
        }
    }
}
