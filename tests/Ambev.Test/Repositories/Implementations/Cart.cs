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
            var validProducts = products?.Where(p => p != null && p.Quantity > 0 && !string.IsNullOrWhiteSpace(p.Subsidiary)).ToList();

            if (validProducts == null || !validProducts.Any())
            {
                throw new Exception("Erro: Nenhum produto válido foi gerado para o carrinho.");
            }

            return new CartDomain(
                f.Random.Int(),
                userId > 0 ? userId : f.Random.Int(1, 1000), 
                date <= DateTime.Now ? date : f.Date.Past(), 
                cancel,
                validProducts
            );
        });

        return faker.Generate(generate);
    }
}

