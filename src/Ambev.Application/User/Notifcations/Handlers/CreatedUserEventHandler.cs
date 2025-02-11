using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.Application.User.Notifcations.Handlers;

public class CreatedUserEventHandler : INotificationHandler<UserEvent>
{
    private readonly ILogger<CreatedUserEventHandler> _logger;

    public CreatedUserEventHandler(ILogger<CreatedUserEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(UserEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User created: {notification.UserDomain.Id} em {notification.UserDomain.Name}");
        await Task.CompletedTask;
    }
}
