namespace Ambev.Domain.ValueObjects;

public sealed class ExternalIdentityDomain
{
    public ExternalIdentityDomain(string provider, string externalId)
    {

        Provider = provider;
        ExternalId = externalId;
    }

    public string Provider { get; private set; }
    public string ExternalId { get; private set; }
}
