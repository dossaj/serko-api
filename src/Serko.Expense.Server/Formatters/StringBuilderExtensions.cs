using System.Text;

namespace Serko.Expense.Server.Formatters
{
    public static class StringBuilderExtensions
    {
        public static Keyword TagKeyword(this StringBuilder builder, bool open)
        {
            var text = builder.ToString();
            var type = open ? KeywordType.OpeningTag : KeywordType.ClosingTag;
            builder.Clear();

            return new Keyword(type, text);
        }
        
        public static Keyword TextKeyword(this StringBuilder builder)
        {
            var text = builder.ToString();
            builder.Clear();

            return new Keyword(KeywordType.Text, text);
        }

        public static Keyword EmailKeyword(this StringBuilder builder)
        {
            return new Keyword(KeywordType.Email, builder.ToString());
        }
    }
}