using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serko.Expense.Server.Formatters
{
    public class EmailXmlLexer : IEnumerable<Keyword>
    {
        private readonly TextReader reader;

        public EmailXmlLexer(TextReader reader)
        {
            this.reader = reader;
        }

        public IEnumerator<Keyword> GetEnumerator()
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                foreach (var keyword in Parse(line))
                {
                    yield return keyword;
                }
            }
        }

        public IEnumerable<Keyword> Parse(string line)
        {
            var open = false;
            var builder = new StringBuilder(line.Length);
            foreach (var c in line)
            {
                switch (c)
                {
                    case ':':
                    {
                        yield return builder.EmailKeyword();
                        yield break;
                    }
                    case '<':
                    {
                        if (builder.Length > 0)
                        {
                            yield return builder.TextKeyword();
                        }

                        builder.Append(c);
                        open = true;
                        break;
                    }
                    case '>':
                    {
                        builder.Append(c);
                        yield return builder.TagKeyword(open);
                        break;
                    }
                    case '/':
                    {
                        builder.Append(c);
                        open = false;
                        break;
                    }
                    default:
                        builder.Append(c);
                        break;
                }
            }

            if (builder.Length > 0)
            {
                yield return builder.TextKeyword();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}