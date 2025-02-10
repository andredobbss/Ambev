namespace Ambev.Domain.ValueObjects;

public sealed class ProductRatingDomain
{
    protected ProductRatingDomain() { }

    public ProductRatingDomain(double rate, int count)
    {
        Rate = rate;
        Count = count;
    }

    public double Rate { get; private set; }
    public int Count { get; private set; }
}
