using Ambev.Domain.Entities;
using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;

namespace Ambev.Test.Repositories.Implementations;

public class CartProduct : ICartProduct
{
    public List<CartProductDomain> CartProductFake(List<ProductDomain> products, int generate)
    {
        var faker = new Faker<CartProductDomain>()
         .CustomInstantiator(f =>
         {
             var product = f.PickRandom(products); 

             return new CartProductDomain(
                 product,
                 f.Random.Int(1, 20),
                 f.Company.CompanyName() 
             );
         });

        return faker.Generate(generate);
    }
}
