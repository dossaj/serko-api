using System.IO;
using System.Linq;
using Serko.Expense.Core.Serialization;
using Xunit;

namespace Serko.Expense.Tests
{
    public class EmailXmlLexerTests
    {
        [Fact]
        public void Enumerable_TagWithValue_CorrectKeywordsReturned()
        {
            //arrange
            var reader = new StringReader("<test>text</test>");
            var lexer = new EmailXmlLexer(reader);

            //act
            var result = lexer.ToArray();

            //assert
            Assert.Equal("<test>", result[0].Value);
            Assert.Equal("text", result[1].Value);
            Assert.Equal("</test>", result[2].Value);

            Assert.Equal(KeywordType.OpeningTag, result[0].KeywordType);
            Assert.Equal(KeywordType.Text, result[1].KeywordType);
            Assert.Equal(KeywordType.ClosingTag, result[2].KeywordType);
        }

        [Fact]
        public void Enumerable_TextBeforeTagWithValue_CorrectKeywordsReturned()
        {
            //arrange
            var reader = new StringReader("asd<test>text</test>");
            var lexer = new EmailXmlLexer(reader);

            //act
            var result = lexer.ToArray();

            //assert
            Assert.Equal("asd", result[0].Value);
            Assert.Equal("<test>", result[1].Value);
            Assert.Equal("text", result[2].Value);
            Assert.Equal("</test>", result[3].Value);

            Assert.Equal(KeywordType.Text, result[0].KeywordType);
            Assert.Equal(KeywordType.OpeningTag, result[1].KeywordType);
            Assert.Equal(KeywordType.Text, result[2].KeywordType);
            Assert.Equal(KeywordType.ClosingTag, result[3].KeywordType);
        }

        [Fact]
        public void Enumerable_TextAfterTagWithValue_CorrectKeywordsReturned()
        {
            //arrange
            var reader = new StringReader("<test>text</test>asd");
            var lexer = new EmailXmlLexer(reader);

            //act
            var result = lexer.ToArray();

            //assert
            Assert.Equal("<test>", result[0].Value);
            Assert.Equal("text", result[1].Value);
            Assert.Equal("</test>", result[2].Value);
            Assert.Equal("asd", result[3].Value);

            Assert.Equal(KeywordType.OpeningTag, result[0].KeywordType);
            Assert.Equal(KeywordType.Text, result[1].KeywordType);
            Assert.Equal(KeywordType.ClosingTag, result[2].KeywordType);
            Assert.Equal(KeywordType.Text, result[3].KeywordType);
        }

        [Fact]
        public void Enumerable_TextBeforeAndAfterTagWithValue_CorrectKeywordsReturned()
        {
            //arrange
            var reader = new StringReader("asd<test>text</test>asd");
            var lexer = new EmailXmlLexer(reader);

            //act
            var result = lexer.ToArray();

            //assert
            Assert.Equal("asd", result[0].Value);
            Assert.Equal("<test>", result[1].Value);
            Assert.Equal("text", result[2].Value);
            Assert.Equal("</test>", result[3].Value);
            Assert.Equal("asd", result[4].Value);

            Assert.Equal(KeywordType.Text, result[0].KeywordType);
            Assert.Equal(KeywordType.OpeningTag, result[1].KeywordType);
            Assert.Equal(KeywordType.Text, result[2].KeywordType);
            Assert.Equal(KeywordType.ClosingTag, result[3].KeywordType);
            Assert.Equal(KeywordType.Text, result[4].KeywordType);
        }

        [Fact]
        public void Enumerable_TextBeforeAndAfterWithNestedTagAndValue_CorrectKeywordsReturned()
        {
            //arrange
            var reader = new StringReader("asd<test><inner>text</inner></test>asd");
            var lexer = new EmailXmlLexer(reader);

            //act
            var result = lexer.ToArray();

            //assert
            Assert.Equal("asd", result[0].Value);
            Assert.Equal("<test>", result[1].Value);
            Assert.Equal("<inner>", result[2].Value);
            Assert.Equal("text", result[3].Value);
            Assert.Equal("</inner>", result[4].Value);
            Assert.Equal("</test>", result[5].Value);
            Assert.Equal("asd", result[6].Value);

            Assert.Equal(KeywordType.Text, result[0].KeywordType);
            Assert.Equal(KeywordType.OpeningTag, result[1].KeywordType);
            Assert.Equal(KeywordType.OpeningTag, result[2].KeywordType);
            Assert.Equal(KeywordType.Text, result[3].KeywordType);
            Assert.Equal(KeywordType.ClosingTag, result[4].KeywordType);
            Assert.Equal(KeywordType.ClosingTag, result[5].KeywordType);
            Assert.Equal(KeywordType.Text, result[6].KeywordType);
        }

        [Fact]
        public void Enumerable_EmailField_TagIsIgnoredAndCorrectKeywordsReturned()
        {
            //arrange
            var reader = new StringReader("To: Antoine Lloyd <Antoine.Lloyd@example.com>");
            var lexer = new EmailXmlLexer(reader);

            //act
            var result = lexer.ToArray();

            //assert
            Assert.Equal("To", result[0].Value);
            Assert.Equal(KeywordType.Email, result[0].KeywordType);
        }
    }
}