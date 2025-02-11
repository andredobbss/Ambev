using Ambev.Application.Interfaces;
using Ambev.Application.User.Notifcations;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.User.Commands.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDomain>
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;
    public DeleteUserCommandHandler(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }

    public async Task<UserDomain> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deleteUser = await _userService.DeleteUserAsync(request.Id);

            await _mediator.Publish(deleteUser, cancellationToken);

            await _mediator.Publish(new UserEvent(deleteUser), cancellationToken);

            return deleteUser;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
