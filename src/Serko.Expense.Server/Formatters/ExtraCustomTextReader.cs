using System.Collections.Generic;
using System.IO;

namespace Serko.Expense.Server.Formatters
{
    public class ExtraCustomTextReader : StreamReader
    {
        private readonly IEnumerable<Keyword> lexer;

        public ExtraCustomTextReader(Stream stream, IEnumerable<Keyword> lexer)
            : base(stream)
        {
            this.lexer = lexer;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            for (var i = index; i < count; ++i)
            {
            }

            return count;
        }
    }
}