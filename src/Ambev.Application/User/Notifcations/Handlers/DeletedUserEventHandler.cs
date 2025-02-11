using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.User.Notifcations.Handlers;

public class DeletedUserEventHandler : INotificationHandler<UserEvent>
{
    private readonly ILogger<DeletedUserEventHandler> _logger;

    public DeletedUserEventHandler(ILogger<DeletedUserEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(UserEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User deleted: {notification.UserDomain.Id} em {notification.UserDomain.Name}");
        await Task.CompletedTask;
    }
}
