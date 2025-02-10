using Ambev.Domain.Entities;
using Ambev.Domain.Entities.Auth;

namespace Ambev.Domain.Repositories.Auth;

public interface IAuthenticationRepository
{
    AuthenticationUserDomain GenerateJwtToken(UserDomain user);
}
