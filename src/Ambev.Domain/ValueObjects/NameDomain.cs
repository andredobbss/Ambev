namespace Ambev.Domain.ValueObjects;

public sealed class NameDomain
{
    protected NameDomain() { }

    public NameDomain(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
}
