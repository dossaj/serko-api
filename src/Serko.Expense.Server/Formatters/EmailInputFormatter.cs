using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Serko.Expense.Core.Serialization;
using Serko.Expense.Server.Dtos;

namespace Serko.Expense.Server.Formatters;

public class EmailInputFormatter : TextInputFormatter
{
    private readonly ILogger logger;

    public EmailInputFormatter(ILogger logger)
    {
        this.logger = logger;

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
        SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/email"));
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(SaveReservationDto);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var request = context.HttpContext.Request;
        request.EnableBuffering();

        using var stream = new StreamReader(request.Body, leaveOpen: true);
        var lexer = await new EmailXmlLexer(stream)
            .ToListAsync();
        
        using var emailReader = new EmailXmlTextReader(lexer, context.ModelType);

        var serializer = new XmlSerializer(context.ModelType);
        try
        {
            var model = serializer.Deserialize(emailReader);
            return await InputFormatterResult.SuccessAsync(model);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogInformation(ex.Message, ex);
            context.ModelState.TryAddModelException("email", ex);
            return await InputFormatterResult.FailureAsync();
        }
    }
}