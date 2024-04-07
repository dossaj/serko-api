using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Serko.Expense.Core.Extensions;

namespace Serko.Expense.Core.Serialization;

public class EmailXmlLexer : IAsyncEnumerable<Keyword>
{
    private readonly TextReader reader;

    private static readonly string[] EmailStrings = {"To", "Subject", "From", "Cc"};

    public EmailXmlLexer(TextReader reader)
    {
        this.reader = reader;
    }

    public async IAsyncEnumerator<Keyword> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        string line;
        while ((line = await reader.ReadLineAsync()) != null)
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
                    if (IsEmailKeyword(builder))
                    {
                        yield return builder.EmailKeyword();
                        yield break;
                    }
                    break;
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

    private bool IsEmailKeyword(StringBuilder builder)
    {
        var text = builder
            .ToString()
            .Trim();
        return EmailStrings.Contains(text);
    }
}