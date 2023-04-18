using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Notification
{
    public sealed record UserLoggedOutNotification(User User) : INotification;
}
