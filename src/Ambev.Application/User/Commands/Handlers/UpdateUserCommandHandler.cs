using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Enums;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.User.Commands.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDomain>
{
    private readonly IUserService _userService;

    public UpdateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDomain> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userDomain = new UserDomain(
                request.Id,
                request.Email,
                request.Username,
                request.Password,
                new NameDomain(request.Name.FirstName, request.Name.LastName),
                new AddressDomain(request.Address.City, request.Address.Street, request.Address.Number, request.Address.ZipCode, request.Address.Phone,
                new GeolocationDomain(request.Address.Geolocation.Lat, request.Address.Geolocation.Long)),
                (EUserStatusDomain)request.Status,
                (EUserRoleDomain)request.Role,
                new ExternalIdentityDomain(request.ExternalIdentity.Provider, request.ExternalIdentity.ExternalId));

            var updaterUser = await _userService.UpdateUserAsync(userDomain);

            return updaterUser;
        }
        catch (DomainValidationException)
        {

            throw;
        }
    }
}
