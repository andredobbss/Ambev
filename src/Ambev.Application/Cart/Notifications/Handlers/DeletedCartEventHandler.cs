using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.Cart.Notifications.Handlers;

public class DeletedCartEventHandler : INotificationHandler<CartEvent>
{
    private readonly ILogger<DeletedCartEventHandler> _logger;

    public DeletedCartEventHandler(ILogger<DeletedCartEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CartEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Cart dleted: {notification.CartDomain.Id} em {notification.CartDomain.TotalSold}");
        await Task.CompletedTask;
    }
}
