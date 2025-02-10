using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.Enums;
using Ambev.Domain.Validations;
using Ambev.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.User.Commands.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDomain>
{
    private readonly IUserService _userService;
    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDomain> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
         
            var userDomain = new UserDomain(
                0, 
                request.Email, 
                request.Username,
                request.Password,
                new NameDomain(request.Name.FirstName, request.Name.LastName), 
                new AddressDomain(request.Address.City, request.Address.Street, request.Address.Number, request.Address.ZipCode, request.Address.Phone,
                new GeolocationDomain(request.Address.Geolocation.Lat, request.Address.Geolocation.Long)), 
                (EUserStatusDomain)request.Status, 
                (EUserRoleDomain)request.Role, 
                new ExternalIdentityDomain(request.ExternalIdentity.Provider, request.ExternalIdentity.ExternalId));

            var createdUser = await _userService.RegisterUserAsync(userDomain);

            return createdUser;
        }
        catch (DomainValidationException)
        {
            throw;
        }
    }
}
