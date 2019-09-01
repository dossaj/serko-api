using TechTalk.SpecFlow;

namespace Serko.Expense.Specs.Steps
{
    [Binding]
    public class BackgroundSteps
    {
        private readonly ApiContext context;

        public BackgroundSteps(ApiContext context)
        {
            this.context = context;
        }
    }
}