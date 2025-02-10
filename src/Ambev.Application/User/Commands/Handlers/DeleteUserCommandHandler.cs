using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.User.Commands.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDomain>
{
    private readonly IUserService _userService;
    public DeleteUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDomain> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deleteUser = await _userService.DeleteUserAsync(request.Id);

            return deleteUser;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
