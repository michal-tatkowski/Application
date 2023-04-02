using Core.Domain.Models;
using Core.Enums.Shared;
using Core.EventLog.EventLog.Command.AddNewEventLog;
using MediatR;

namespace Core.EventLog.EventLog.Notification.AddInformationEventLog
{
    public record AddInformationEventLogNotification : INotification
    {
        public string Message { get; set; }
        public PermissionTypeEnum PermissionTypeEnum { get; set; }
        public int UserId { get; set; }

        public AddInformationEventLogNotification(string message, PermissionTypeEnum permissionTypeEnum)
        {
            Message = message;
            PermissionTypeEnum = permissionTypeEnum;
        }

        public AddInformationEventLogNotification(string message, PermissionTypeEnum permissionTypeEnum, int userId)
        {
            Message = message;
            PermissionTypeEnum = permissionTypeEnum;
            UserId = userId;
        }
    }

    internal sealed class AddInformationEventLogNotificationHandler : INotificationHandler<AddInformationEventLogNotification>
    {
        private readonly IMediator mediator;

        public AddInformationEventLogNotificationHandler(IMediator mediator) 
        { 
            this.mediator = mediator;
        }

        public async Task Handle(AddInformationEventLogNotification notification, CancellationToken cancellationToken)
        {
            var eventLog = new ApiEventLog
            {
                Message = notification.Message,
                PermissionTypeEnum = notification.PermissionTypeEnum.Value,
                EventLogInformationTypeEnum = EventLogInformationTypeEnum.Event,
                Date = DateTime.Now,
                UserId = notification.UserId,
            };
            var command = new AddNewEventLogCommand(eventLog);
            await mediator.Send(command, cancellationToken).ConfigureAwait(false);
        }
    }
}
