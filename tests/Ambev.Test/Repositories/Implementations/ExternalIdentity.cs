using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;

namespace Ambev.Test.Repositories.Implementations;

public class ExternalIdentity : IExternalIdentity
{
    public ExternalIdentityDomain ExternalIdentityFake()
    {
        var faker = new Faker<ExternalIdentityDomain>()
           .CustomInstantiator(f => new ExternalIdentityDomain(
               f.Company.CompanyName(),
               f.Random.Guid().ToString()
           ));

        return faker.Generate(1).First();
    }
}
