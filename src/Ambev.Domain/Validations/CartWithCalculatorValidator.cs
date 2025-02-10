using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class CartWithCalculatorValidator : AbstractValidator<CartWithCalculatorDomain>
{
    public CartWithCalculatorValidator()
    {
        RuleFor(x => x.PriceWithDiscount).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.Discount).InclusiveBetween(0, 1).WithMessage(ResourceMessagesException.DISCOUNT);
        RuleFor(x => x.DiscountMessage).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);

    }
}
