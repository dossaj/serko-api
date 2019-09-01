using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace Serko.Expense.Specs.Steps
{
    [Binding]
    public class AuthorizationSteps
    {
        private const string Token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SpV1qCvvB4JQ1Mz7aghLqTzJi12KvIMPrpAtu9OBiVg";

        private readonly ApiContext context;

        public AuthorizationSteps(ApiContext context)
        {
            this.context = context;
        }

        [Given(@"I have an invalid token")]
        public void GivenIHaveAnInvalidToken()
        {
            context.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "invalid token");
        }

        [Given(@"I have an valid token")]
        public void GivenIHaveAnValidToken()
        {
            context.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
        }
    }
}