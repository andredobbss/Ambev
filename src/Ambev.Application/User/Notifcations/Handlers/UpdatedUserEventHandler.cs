using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.User.Notifcations.Handlers;

public class UpdatedUserEventHandler : INotificationHandler<UserEvent>
{
    private readonly ILogger<UpdatedUserEventHandler> _logger;

    public UpdatedUserEventHandler(ILogger<UpdatedUserEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(UserEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User updated: {notification.UserDomain.Id} em {notification.UserDomain.Name}");
        await Task.CompletedTask;
    }
}
