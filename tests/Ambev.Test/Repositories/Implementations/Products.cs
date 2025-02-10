using Ambev.Domain.Entities;
using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;
namespace Ambev.Test.Repositories.Implementations;

public class Products : IProducts
{

    public List<ProductDomain> ProdcutsFake(int generate)
    {
        var faker = new Faker<ProductDomain>()
           .CustomInstantiator(f => new ProductDomain(
               f.Random.Int(),
               f.Commerce.ProductName().PadRight(5, 'X'), 
               Math.Round(f.Random.Decimal(1, 1000), 2), 
               f.Commerce.ProductDescription() ?? "Default Description",
               f.Commerce.Categories(1).First() ?? "Default Category",
               f.Image.PicsumUrl(),
               new ProductRatingDomain(f.Random.Double(1, 5), f.Random.Int(1, 1000))
           ));

        return faker.Generate(generate);
    }
}
