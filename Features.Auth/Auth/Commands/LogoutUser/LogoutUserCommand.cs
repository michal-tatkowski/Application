using Core.Application.Interfaces;
using Core.Application.Notification;
using Core.Domain.Entities;
using Core.Enums.Shared;
using Core.EventLog.EventLog.Notification.AddInformationEventLog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Features.Auth.Auth.Commands.LogoutUser
{
    public record LogoutUserCommand(bool IsAutomaticLogout, User User) : IRequest;

    internal sealed class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IMediator mediator;
        private readonly IDbContext dbContext;

        public LogoutUserCommandHandler(IMediator mediator, IDbContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }

        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;

            var loggedOutNotification = new UserLoggedOutNotification(user);
            await mediator.Publish(loggedOutNotification, cancellationToken).ConfigureAwait(false);

            var userSession = await GetUserSessionAsync(user, cancellationToken).ConfigureAwait(false);

            if (userSession is not null) userSession.LastActivity = null;

            await GenerateAndAddLog(user, request.IsAutomaticLogout, cancellationToken).ConfigureAwait(false);
        }

        private async Task<UserSession> GetUserSessionAsync(User user, CancellationToken cancellationToken)
        {
            return await dbContext.Set<UserSession>()
                .Where(x => x.UserId == user.Id)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task GenerateAndAddLog(User user, bool IsAutomaticLogout, CancellationToken cancellationToken)
        {
            var logMessage = IsAutomaticLogout
                ? new StringBuilder($"Użytkownik {user.Login} (id: {user.Id}) automatycznie wylogował się z powodu bezczyności")
                : new StringBuilder($"Użytkownik {user.Login} (id: {user.Id}) wylogował się");

            var notification = new AddInformationEventLogNotification(logMessage.ToString(), PermissionTypeEnum.System, user.Id);
            await mediator.Publish(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
