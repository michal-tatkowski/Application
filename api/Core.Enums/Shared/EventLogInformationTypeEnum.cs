using Ardalis.SmartEnum;

namespace Core.Enums.Shared
{
    public class EventLogInformationTypeEnum : SmartEnum<EventLogInformationTypeEnum>
    {
        public static readonly EventLogInformationTypeEnum Event = new(nameof(Event), 1);
        public static readonly EventLogInformationTypeEnum Error = new(nameof(Error), 2);

        public EventLogInformationTypeEnum(string name, int value) : base(name, value) { }
    }
}
