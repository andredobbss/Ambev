using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.Cart.Notifications;

public class CartEvent : INotification
{
    public CartEvent(CartDomain cartDomain)
    {
        CartDomain = cartDomain;
    }

    public CartDomain CartDomain { get; }
}
