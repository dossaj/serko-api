using System.Threading.Tasks;

namespace Serko.Expense.Core.Cqrs;

public interface IHandleQuery<in TArguments, TResult>
{
    Task<TResult> Execute(TArguments arguments);
}