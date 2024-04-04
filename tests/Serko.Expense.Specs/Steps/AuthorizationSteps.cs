using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace Serko.Expense.Specs.Steps
{
    [Binding]
    public class AuthorizationSteps
    {
        private const string Token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJjbGllbnRpZCIsImF1ZCI6ImNsaWVudGlkIiwic3ViIjoiMTIzIiwiYSI6IjQ1NiIsImlhdCI6MTYyMTc5OTU5OCwiZXhwIjoxNjIxNzk5NjU4fQ.eymBE5RxtboMcPeNR4POoWepRPxHsKrRcEviEWbe06w";

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