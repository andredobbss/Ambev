using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class GeolocationValidator : AbstractValidator<GeolocationDomain>
{
    public GeolocationValidator()
    {
        RuleFor(x => x.Lat).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(50).WithMessage(ResourceMessagesException.LENGTH__50);
        RuleFor(x => x.Long).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(50).WithMessage(ResourceMessagesException.LENGTH__50);
    }
}
