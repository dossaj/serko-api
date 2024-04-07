using System.Collections.Generic;
using System.Net.Http;

namespace Serko.Expense.Specs;

public class ApiContext
{
    public HttpClient Client { get; set; }
    public HttpResponseMessage Response { get; set; }
    public Dictionary<string, object> Reservation { get; set; }
}