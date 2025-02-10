using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;


public class CartProductValidator : AbstractValidator<CartProductDomain>
{
    public CartProductValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.ProductId).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0).WithMessage(ResourceMessagesException.NUMBER_GREATER_THAN_ZERO).WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.Price).NotEmpty().PrecisionScale(18, 2, true).WithMessage(ResourceMessagesException.INVALID_NUMBER).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.Category).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
        RuleFor(x => x.Subsidiary).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(100).WithMessage(ResourceMessagesException.LENGTH__100);
    }
    
}




