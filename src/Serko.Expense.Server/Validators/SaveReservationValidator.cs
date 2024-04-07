using FluentValidation;
using Serko.Expense.Server.Dtos;

namespace Serko.Expense.Server.Validators;

public class SaveReservationValidator : AbstractValidator<SaveReservationDto>
{
    public SaveReservationValidator()
    {
        RuleFor(x => x.Expense).NotNull();
        RuleFor(x => x.Expense.Total).NotEmpty();
    }
}
