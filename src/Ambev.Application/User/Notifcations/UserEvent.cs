using Ambev.Domain.Entities;
using MediatR;

namespace Ambev.Application.User.Notifcations;

public class UserEvent : INotification
{
    public UserEvent(UserDomain userDomain)
    {
        UserDomain = userDomain;
    }

    public UserDomain UserDomain { get; }
}
