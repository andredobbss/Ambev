using Ambev.Domain.Entities;
using Ambev.Domain.Resourcers;
using FluentValidation;

namespace Ambev.Domain.Validations;

public class UserValidator : AbstractValidator<UserDomain>
{
    public UserValidator()
    { 
        RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_EMAIL).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
        RuleFor(x => x.Username).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_FIELD).NotNull().WithMessage(ResourceMessagesException.NULL_FIELD).MaximumLength(255).WithMessage(ResourceMessagesException.LENGTH__255);
        RuleFor(x => x.Password)
           .NotEmpty().WithMessage(ResourceMessagesException.EMPTY_PASSWORD)
           .MinimumLength(8).WithMessage(ResourceMessagesException.PASSWORD_MIN_LENGTH)
           .MaximumLength(20).WithMessage(ResourceMessagesException.PASSWORD_MAX_LENGTH)
           .Matches(@"[A-Z]").WithMessage(ResourceMessagesException.PASSWORD_UPPERCASE_CHARACTERS)
           .Matches(@"[a-z]").WithMessage(ResourceMessagesException.PASSWORD_LOWERCASE_CHARACTERS)
           .Matches(@"\d").WithMessage(ResourceMessagesException.PASSWORD_NUMBER)
           .Matches(@"[\W_]").WithMessage(ResourceMessagesException.PASSWORD_SPECIAL_CHARACTER);
        RuleFor(x => x.Name).SetValidator(new NameValidator());
        RuleFor(x => x.Address).SetValidator(new AddressValidator());
        RuleFor(x => x.ExternalIdentity).SetValidator(new ExternalIdentityValidator());
    
    }
}
