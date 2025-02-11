using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.Product.Notifications.Handlers;

public class DeletedProductEventHandler : INotificationHandler<ProductEvent>
{
    private readonly ILogger<DeletedProductEventHandler> _logger;

    public DeletedProductEventHandler(ILogger<DeletedProductEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Product deleted: {notification.ProductDomain.Id} em {notification.ProductDomain.Description}");
        await Task.CompletedTask;
    }
}
