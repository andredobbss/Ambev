using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.User.Queries.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDomain>
{
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDomain> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        
        try
        {
            var user = await _userService.GetUserByIdAsync(request.Id);

            return user;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
