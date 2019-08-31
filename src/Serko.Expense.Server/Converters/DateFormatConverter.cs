using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Serko.Expense.Server.Converters
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var value = reader.Value.ToString();
                if (DateTime.TryParseExact(value, "dddd d MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }
            
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}