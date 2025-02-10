using Ambev.Domain.Entities;
using Ambev.Domain.Enums;
using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;

namespace Ambev.Test.Repositories.Implementations;

public class User : IUser
{
    public List<UserDomain> UserFake(int id, string email, string username, string password, NameDomain name, AddressDomain address, EUserStatusDomain status, EUserRoleDomain role, ExternalIdentityDomain externalIdentity, int generate)
    {
       
        var faker = new Faker<UserDomain>()

       .CustomInstantiator(f =>
       {
           

           return new UserDomain(
               id,
               email,
               username,
               password,
               name,
               address,
               status,
               role,
               externalIdentity          
           );
       });

        return faker.Generate(generate);
    }
}
