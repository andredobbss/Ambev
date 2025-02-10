using Ambev.Application.Interfaces;
using Ambev.Domain.Entities.Auth;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.User.Commands.Handlers;

public class AuthenticationUserCommandHandler : IRequestHandler<AuthenticationUserCommand, AuthenticationUserDomain>
{
    private readonly IUserService _userService;

    public AuthenticationUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AuthenticationUserDomain> Handle(AuthenticationUserCommand request, CancellationToken cancellationToken)
    {
        try
        {        
            var authenticatioUserDomain = await _userService.Login(request.Username, request.Password);

            return authenticatioUserDomain;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
