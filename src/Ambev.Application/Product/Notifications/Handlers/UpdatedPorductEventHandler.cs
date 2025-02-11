using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.Product.Notifications.Handlers;

public class UpdatedPorductEventHandler : INotificationHandler<ProductEvent>
{
    private readonly ILogger<UpdatedPorductEventHandler> _logger;

    public UpdatedPorductEventHandler(ILogger<UpdatedPorductEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Product updated: {notification.ProductDomain.Id} em {notification.ProductDomain.Description}");
        await Task.CompletedTask;
    }
}
