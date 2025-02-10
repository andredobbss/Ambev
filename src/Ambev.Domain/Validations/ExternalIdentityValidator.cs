using Ambev.Domain.Resourcers;
using Ambev.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class ExternalIdentityValidator : AbstractValidator<ExternalIdentityDomain>
{
    public ExternalIdentityValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
        RuleFor(x => x.Provider).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
    }
}
