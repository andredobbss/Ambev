using FluentValidation.Results;

namespace Ambev.Domain.Validations;

public class DomainValidationException : Exception
{
    public List<ValidationFailure> Errors { get; }

    public DomainValidationException(string message, List<ValidationFailure> errors)
        : base(message)
    {
        Errors = errors;
    }
}