using Ambev.Domain.Entities;
using Ambev.Domain.Enums;
using Ambev.Domain.ValueObjects;

namespace Ambev.Test.Repositories.Interfaces;

public interface IUser
{
    List<UserDomain> UserFake(int id, string email, string username, string password, NameDomain name, AddressDomain address, EUserStatusDomain status, EUserRoleDomain role, ExternalIdentityDomain externalIdentity, int generate);
}
