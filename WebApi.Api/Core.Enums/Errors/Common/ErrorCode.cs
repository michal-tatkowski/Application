using Ardalis.SmartEnum;

namespace Core.Enums.Errors.Common;

/// <summary>
/// Enum, który jest bazą innych enumów z błędami.
/// </summary>
public abstract class ErrorCode : SmartEnum<ErrorCode>
{
    public abstract string Message { get; }
    public abstract int HttpCode { get; }

    protected ErrorCode(string name, int value) : base(name, value)
    {
    }
}