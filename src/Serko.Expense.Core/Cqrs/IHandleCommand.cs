using System.Threading.Tasks;

namespace Serko.Expense.Core.Cqrs
{
    public interface IHandleCommand<in TArguments>
    {
        Task Execute(TArguments arguments);
    }
}