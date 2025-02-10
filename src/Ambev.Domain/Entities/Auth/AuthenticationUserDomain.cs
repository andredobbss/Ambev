using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using FluentValidation.Results;

namespace Ambev.Domain.Entities.Auth;

public sealed class AuthenticationUserDomain
{ 
    public bool Authenticated { get; set; }
    public DateTime Expiration { get; set; }
    public string Token { get; set; } = null!;
    public string Message { get; set; } = null!;

}
