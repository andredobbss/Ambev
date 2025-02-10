using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class NameValidator : AbstractValidator<NameDomain>
{
    public NameValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(100).WithMessage(ResourceMessagesException.LENGTH__100);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(100).WithMessage(ResourceMessagesException.LENGTH__100);

    }
}
