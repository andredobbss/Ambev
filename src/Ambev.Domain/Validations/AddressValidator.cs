using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class AddressValidator : AbstractValidator<AddressDomain>
{
    public AddressValidator()
    {
        RuleFor(x => x.City).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(100).WithMessage(ResourceMessagesException.LENGTH__100);
        RuleFor(x => x.Street).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
        RuleFor(x => x.Number).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD);
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(20).WithMessage(ResourceMessagesException.LENGTH__20);
        RuleFor(x => x.Phone).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(20).WithMessage(ResourceMessagesException.LENGTH__20);
        RuleFor(x => x.Geolocation).SetValidator(new GeolocationValidator());
    }
}
