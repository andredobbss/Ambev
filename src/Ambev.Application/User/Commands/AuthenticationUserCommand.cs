using Ambev.Domain.Entities.Auth;
using MediatR;

namespace Ambev.Application.User.Commands;

public sealed class AuthenticationUserCommand : IRequest<AuthenticationUserDomain>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
