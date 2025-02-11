using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Product.Notifications;

public class ProductEvent : INotification
{
    public ProductEvent(ProductDomain productDomain)
    {
        ProductDomain = productDomain;
    }

    public ProductDomain ProductDomain { get; }
}
