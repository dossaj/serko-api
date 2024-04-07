using System.Threading.Tasks;

namespace Serko.Expense.Core.Cqrs;

public interface IExecute
{
    Task Command<TArguments>(TArguments arguments);
    Task<TResult> Command<TArguments, TResult>(TArguments arguments);
    Task<TResult> Query<TArguments, TResult>(TArguments arguments);
}