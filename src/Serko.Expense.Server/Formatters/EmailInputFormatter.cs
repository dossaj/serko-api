using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Net.Http.Headers;
using Serko.Expense.Core.Serialization;
using Serko.Expense.Server.Dtos;

namespace Serko.Expense.Server.Formatters
{
    public class EmailInputFormatter : TextInputFormatter
    {
        public EmailInputFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/email"));
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(SaveReservationDto);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;
            
            request.EnableBuffering();

            using (var stream = new StreamReader(new NonDisposableStream(request.Body)))
            using (var text = new EmailXmlTextReader(new EmailXmlLexer(stream), context.ModelType))
            using (var reader = XmlReader.Create(text))
            {
                var serializer = new XmlSerializer(context.ModelType);
                var model = serializer.Deserialize(reader);

                return InputFormatterResult.SuccessAsync(model);
            }
        }
    }
}