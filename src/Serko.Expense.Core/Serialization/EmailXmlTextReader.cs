using System;
using System.Collections.Generic;
using System.IO;

namespace Serko.Expense.Core.Serialization
{
    public class EmailXmlTextReader : TextReader
    {
        private int depth;
        private int position;
        private IEnumerator<Keyword> iterator;

        private readonly Type type;
        private readonly IEnumerable<Keyword> lexer;
        
        public EmailXmlTextReader(IEnumerable<Keyword> lexer, Type type)
        {
            this.lexer = lexer;
            this.type = type;
            Initialise();
        }
        
        public override int Read(char[] buffer, int index, int count)
        {
            if (iterator.Current == null)
            {
                return 0;
            }

            for (var i = index; i < count; ++i)
            {
                if (position == iterator.Current.Value.Length)
                {
                    if (!iterator.MoveNext())
                    {
                        return i - index;
                    }
                    position = 0;
                }
                buffer[i] = iterator.Current.Value[position++];
            }
            return count;
        }

        protected void Initialise()
        {
            iterator = Move().GetEnumerator();
            iterator.MoveNext();
        }

        private IEnumerable<Keyword> Move()
        {
            yield return new Keyword(KeywordType.OpeningTag, $"<{type.Name}>");

            foreach (var keyword in lexer)
            {
                if (ExternalTextOrEmail(keyword.KeywordType))
                {
                    continue;
                }
                UpdateDepth(keyword.KeywordType);
                yield return keyword;
            }

            yield return new Keyword(KeywordType.OpeningTag, $"</{type.Name}>");
        }

        private bool ExternalTextOrEmail(KeywordType keywordType)
        {
            return (depth == 0 && keywordType == KeywordType.Text) || keywordType == KeywordType.Email;
        }

        private void UpdateDepth(KeywordType keywordType)
        {
            if (keywordType == KeywordType.OpeningTag)
            {
                depth++;
            }
            else if (keywordType == KeywordType.ClosingTag)
            {
                depth--;
            }
        }
    }
}