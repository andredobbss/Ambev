using Ambev.Domain.Entities;
using Ambev.Domain.Resourcers;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class CartValidator : AbstractValidator<CartDomain>
{
    
    public CartValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage(ResourceMessagesException.INVALID_IDENTIFIED);
        RuleFor(x => x.Date).NotEmpty().NotNull().Must(date => date != default).WithMessage(ResourceMessagesException.INVALID_DATE);
        RuleFor(x => x.Cancel).Must(value => value == false || value == true).WithMessage(ResourceMessagesException.BOOLEAN);
        RuleFor(x => x.TotalSold).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleForEach(x => x.Products).SetValidator(new CartProductValidator());
        RuleForEach(x => x.CartWithCalculators).SetValidator(new CartWithCalculatorValidator());
    }

}
