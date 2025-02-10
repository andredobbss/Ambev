using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;

namespace Ambev.Test.Repositories.Implementations;

public class NameFaker : INameFaker
{
    public NameDomain NameFake()
    {
        var faker = new Faker<NameDomain>()
           .CustomInstantiator(f => new NameDomain(
               f.Name.FirstName(),
               f.Name.LastName()
           ));

        return faker.Generate(1).First();
    }
}
