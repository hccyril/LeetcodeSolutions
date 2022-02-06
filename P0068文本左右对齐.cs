using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0068文本左右对齐
    {
        class Sentence
        {
            public bool IsLastLine = false;
            readonly int maxWidth;
            readonly IList<string> words = new List<string>();
            int length = 0;

            public Sentence(int maxWidth)
            {
                this.maxWidth = maxWidth;
            }

            public override string ToString()
            {
                if (words.Count == 1 || IsLastLine)
                {
                    return string.Join(' ', words).PadRight(maxWidth);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(words[0]);
                    for (int i = 1; i < words.Count; ++i)
                    {
                        int len = words[i].Length + 1 + (maxWidth - length) / (words.Count - 1);
                        if (i <= (maxWidth - length) % (words.Count - 1)) len++;
                        sb.Append(words[i].PadLeft(len));
                    }
                    return sb.ToString();
                }
            }

            public bool AddWord(string word)
            {
                if (length > 0 && length + word.Length + 1 > maxWidth)
                    return false;
                words.Add(word);
                if (length > 0) length++;
                length += word.Length;
                return true;
            }
        }

        class Paragraph
        {
            readonly int maxWidth;

            readonly IList<Sentence> lines = new List<Sentence>();

            public Paragraph(int maxWidth)
            {
                this.maxWidth = maxWidth;
            }

            public IList<string> ToLines()
            {
                if (!lines.Any()) return new List<string>();
                lines.Last().IsLastLine = true;
                return lines.Select(t => t.ToString()).ToList();
            }

            public void AddWord(string word)
            {
                if (!lines.Any() || !lines.Last().AddWord(word))
                {
                    var line = new Sentence(maxWidth);
                    line.AddWord(word);
                    lines.Add(line);
                }
            }
        }
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            var pa = new Paragraph(maxWidth);
            foreach (var w in words)
                pa.AddWord(w);
            return pa.ToLines();
        }
    }
}
