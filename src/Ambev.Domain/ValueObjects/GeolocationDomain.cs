namespace Ambev.Domain.ValueObjects;

public sealed class GeolocationDomain
{
    protected GeolocationDomain() { }

    public GeolocationDomain(string lat, string @long)
    {
        Lat = lat;
        Long = @long;
    }

    public string Lat { get; private set; } = string.Empty;
    public string Long { get; private set; } = string.Empty;
}
