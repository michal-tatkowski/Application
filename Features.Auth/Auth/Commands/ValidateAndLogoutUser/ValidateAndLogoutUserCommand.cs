using Core.Application.Interfaces;
using Core.Domain.Entities;
using Features.Auth.Auth.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Features.Auth.Auth.Commands.ValidateAndLogoutUser
{
    public sealed record ValidateAndLogoutUserCommand : IRequest
    {
        public bool IsAutomaticLogout { get; set; }
    }

    internal sealed class ValidateAndLogoutUserCommandHandler : IRequestHandler<ValidateAndLogoutUserCommand>
    {
        private readonly IMediator mediator;
        private readonly IDbContext dbContext;
        private readonly IAuthenticatedUserService userService;

        public ValidateAndLogoutUserCommandHandler(IMediator mediator, IDbContext dbContext, IAuthenticatedUserService userService)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public async Task Handle(ValidateAndLogoutUserCommand request, CancellationToken cancellationToken)
        {
            var user = await GetUserAsync(cancellationToken).ConfigureAwait(false);
            await mediator.Publish(new LogoutUserNotification(request.IsAutomaticLogout, user), cancellationToken).ConfigureAwait(false);
        }

        private async Task<User> GetUserAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Set<User>()
                .Where(x => x.Id == userService.UserId)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
