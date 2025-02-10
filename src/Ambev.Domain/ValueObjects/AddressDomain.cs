namespace Ambev.Domain.ValueObjects;

public sealed class AddressDomain
{
    protected AddressDomain() { }

    public AddressDomain(string city, string street, int number, string zipCode, string phone, GeolocationDomain geolocation)
    {
        City = city;
        Street = street;
        Number = number;
        ZipCode = zipCode;
        Phone = phone;
        Geolocation = geolocation;
    }

    public string City { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public int Number { get; private set; }
    public string ZipCode { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public GeolocationDomain Geolocation { get; private set; }

}
