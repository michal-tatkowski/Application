using Core.Domain.Entities;
using Features.Auth.Auth.Commands.LogoutUser;
using MediatR;

namespace Features.Auth.Auth.Notifications
{
    public sealed record LogoutUserNotification(bool IsAutomaticLogout, User User) : INotification;
    
    internal sealed class LogoutUserNotificationHandler : INotificationHandler<LogoutUserNotification>
    {
        private readonly IMediator mediator;

        public LogoutUserNotificationHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(LogoutUserNotification notification, CancellationToken cancellationToken)
        {
            var command = new LogoutUserCommand(notification.IsAutomaticLogout, notification.User);
            await mediator.Send(command, cancellationToken).ConfigureAwait(false);
        }
    }
}
