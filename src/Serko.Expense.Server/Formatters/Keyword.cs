namespace Serko.Expense.Server.Formatters
{
    public class Keyword
    {
        public KeywordType KeywordType { get; set; }
        public string Value { get; set; }

        public Keyword(KeywordType keywordType, string value)
        {
            KeywordType = keywordType;
            Value = value;
        }
    }
}