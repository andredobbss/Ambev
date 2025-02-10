using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class ProductRatingValidator : AbstractValidator<ProductRatingDomain>
{
    public ProductRatingValidator()
    {
        RuleFor(x => x.Rate).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.Count).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
    }
}
