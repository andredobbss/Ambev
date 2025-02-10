using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Validations;
using MediatR;

namespace Ambev.Application.User.Queries.Handlers;

public class GetUserByFilterQueryHandler : IRequestHandler<GetUserByFilterQuery, (IEnumerable<UserDomain> Data, int TotalItems, int TotalPages)>
{
    private readonly IUserService _userService;

    public GetUserByFilterQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<(IEnumerable<UserDomain> Data, int TotalItems, int TotalPages)> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userService.GetUsersByFilterToPagedListAsync(request.FilterValue,request.FilterType,request.PageNumber, request.PageSize, request.OrderBy);

            return users;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
