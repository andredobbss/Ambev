using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.Cart.Notifications.Handlers;

public class CreatedCartEventHandler : INotificationHandler<CartEvent>
{
    private readonly ILogger<CreatedCartEventHandler> _logger;

    public CreatedCartEventHandler(ILogger<CreatedCartEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CartEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Cart created: {notification.CartDomain.Id} em {notification.CartDomain.TotalSold}");
        await Task.CompletedTask;
    }
}
