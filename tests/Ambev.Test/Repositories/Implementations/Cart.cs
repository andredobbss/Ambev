using Ambev.Domain.Entities;
using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;

namespace Ambev.Test.Repositories.Implementations;

public class Cart : ICart
{
    public List<CartDomain> CartFake(List<CartProductDomain> products, int userId, DateTime date, bool cancel, int generate)
    {

        var faker = new Faker<CartDomain>()
        .CustomInstantiator(f =>
        {
            return new CartDomain(
                f.Random.Int(),
                userId > 0 ? userId : f.Random.Int(1, 1000), 
                date <= DateTime.Now ? date : f.Date.Past(), 
                cancel,
                products
            );
        });

        return faker.Generate(generate);
    }
}

