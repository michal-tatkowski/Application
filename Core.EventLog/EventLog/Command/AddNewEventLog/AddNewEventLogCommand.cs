using Core.Application.Interfaces;
using Core.Domain.Models;
using MediatR;

namespace Core.EventLog.EventLog.Command.AddNewEventLog
{
    public record AddNewEventLogCommand : IRequest
    {
        public ApiEventLog ApiEventLog { get; set; }

        public AddNewEventLogCommand(ApiEventLog apiEventLog)
        {
            ApiEventLog = apiEventLog;
        }
    }

    public class AddNewEventLogCommandHandler : IRequest<AddNewEventLogCommand>
    {
        private readonly IMediator mediator;
        private readonly IDbContext dbContext; 
        public AddNewEventLogCommandHandler(IMediator mediator, IDbContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddNewEventLogCommand request, CancellationToken cancellationToken)
        {
            await AddEventLogToDb(request.ApiEventLog, cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(Unit.Value).ConfigureAwait(false);
        }

        private async Task AddEventLogToDb(ApiEventLog apiEventLog, CancellationToken cancellationToken)
        {
            await dbContext.Set<ApiEventLog>().AddAsync(apiEventLog, cancellationToken).ConfigureAwait(false);
        }
    }
}
