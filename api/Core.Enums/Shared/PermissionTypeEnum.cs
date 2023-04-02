using Ardalis.SmartEnum;

namespace Core.Enums.Shared
{
    public class PermissionTypeEnum : SmartEnum<PermissionTypeEnum>
    {
        public static readonly PermissionTypeEnum System = new(nameof(System), 1);

        public PermissionTypeEnum(string name, int value) : base (name, value) { }
    }
}
