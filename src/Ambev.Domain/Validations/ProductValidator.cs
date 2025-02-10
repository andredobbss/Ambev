using Ambev.Domain.Entities;
using Ambev.Domain.Resourcers;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class ProductValidator : AbstractValidator<ProductDomain>
{
    public ProductValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0).WithMessage(ResourceMessagesException.NUMBER_GREATER_THAN_ZERO).PrecisionScale(18, 2, true).WithMessage(ResourceMessagesException.INVALID_NUMBER).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.Description).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(500).WithMessage(ResourceMessagesException.LENGTH__500);
        RuleFor(x => x.Category).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(100).WithMessage(ResourceMessagesException.LENGTH__100);
        RuleFor(x => x.Image).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(100).WithMessage(ResourceMessagesException.LENGTH__100);
        RuleFor(x => x.Rating).SetValidator(new ProductRatingValidator());
    }
}
