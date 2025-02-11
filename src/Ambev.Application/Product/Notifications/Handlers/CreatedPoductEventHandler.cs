using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.Product.Notifications.Handlers;

public class CreatedPoductEventHandler : INotificationHandler<ProductEvent>
{
    private readonly ILogger<CreatedPoductEventHandler> _logger;

    public CreatedPoductEventHandler(ILogger<CreatedPoductEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Product created: {notification.ProductDomain.Id} em {notification.ProductDomain.Description}");
        await Task.CompletedTask;
    }
}
