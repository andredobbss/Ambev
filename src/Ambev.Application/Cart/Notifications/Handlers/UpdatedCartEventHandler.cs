using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.Cart.Notifications.Handlers;

public class UpdatedCartEventHandler : INotificationHandler<CartEvent>
{
    private readonly ILogger<UpdatedCartEventHandler> _logger;

    public UpdatedCartEventHandler(ILogger<UpdatedCartEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CartEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User updated: {notification.CartDomain.Id} em {notification.CartDomain.TotalSold}");
        await Task.CompletedTask;
    }
}
