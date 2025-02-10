using Ambev.Domain.ValueObjects;
using Ambev.Test.Repositories.Interfaces;
using Bogus;

namespace Ambev.Test.Repositories.Implementations;

public class AddressFaker : IAddressFaker
{
    public AddressDomain AddressFake()
    {
        var faker = new Faker<AddressDomain>()
           .CustomInstantiator(f => new AddressDomain(
              f.Address.City(),
                f.Address.StreetName(),
                f.Random.Int(1, 9999),
                f.Address.ZipCode(),
                f.Phone.PhoneNumber("(##) #####-####"),
                geolocation: new GeolocationDomain(
                    f.Address.Latitude().ToString(),
                    f.Address.Longitude().ToString()
                ))
           );

        return faker.Generate(1).First();
    }
}
