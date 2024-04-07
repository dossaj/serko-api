using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serko.Expense.Core.Serialization;
using Serko.Expense.Server.Dtos;
using Xunit;

namespace Serko.Expense.Tests
{
    public class EmailXmlTextReaderTests
    {
        [Fact]
        public async Task Read_TagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("<test>text</test>");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer, 0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test>text</test></SaveReservationDto>", str);
        }

        [Fact]
        public async Task Read_TextAfterTagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("<test>text</test>asd");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer,0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test>text</test></SaveReservationDto>", str);
        }

        [Fact]
        public async Task Read_TextBeforeTagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("asd<test>text</test>");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer, 0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test>text</test></SaveReservationDto>", str);
        }

        [Fact]
        public async Task Read_TextBeforeAndAfterTagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("asd<test>text</test>asd");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer, 0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test>text</test></SaveReservationDto>", str);
        }

        [Fact]
        public async Task Read_TextBeforeAndAfterWithNestedTagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("asd<test><inner>text</inner></test>asd");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer, 0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test><inner>text</inner></test></SaveReservationDto>", str);
        }

        [Fact]
        public async Task Read_EmailFieldWithTagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("To: Antoine Lloyd <Antoine.Lloyd@example.com>\r\nasd<test>text</test>asd");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer, 0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test>text</test></SaveReservationDto>", str);
        }

        [Fact]
        public async Task Read_BufferIsCorrectlyRolled_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[5];
            var output = new StringBuilder();
            var reader = new StringReader("asd<test><inner>text</inner></test>asd");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            while (count != 0)
            {
                output.Append(buffer, 0, count);

                buffer = new char[5];
                count = textReader.Read(buffer, 0, buffer.Length);
            }

            //assert
            Assert.Equal("<SaveReservationDto><test><inner>text</inner></test></SaveReservationDto>", output.ToString());
        }

        [Fact]
        public async Task Read_EmailFieldWithAndTextWithColonTagWithValue_CorrectXmlReturned()
        {
            //arrange
            var buffer = new char[100];
            var reader = new StringReader("To: Antoine Lloyd <Antoine.Lloyd@example.com>\r\nasd: <test>text</test>asd");
            var lexer = await new EmailXmlLexer(reader).ToArrayAsync();
            var textReader = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));

            //act
            var count = textReader.Read(buffer, 0, buffer.Length);
            var str = new string(buffer, 0, count);

            //assert
            Assert.Equal("<SaveReservationDto><test>text</test></SaveReservationDto>", str);
        }
    }
}