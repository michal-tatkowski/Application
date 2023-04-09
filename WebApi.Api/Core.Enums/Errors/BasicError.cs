using System.Net;
using Core.Enums.Errors.Common;

namespace Core.Enums.Errors;

/// <summary>
/// Zbiór podstawowych błędów, które mogą wystąpić w aplikacji
/// </summary>
public sealed class BasicError : ErrorCode
{
    public static readonly ErrorCode ERR_UNKNOWN = new ErrorUnknown();
    public static readonly ErrorCode ERR_VALIDATION = new ErrorValidation();
    public static readonly ErrorCode ERR_INVALID_CREDENTIALS = new ErrorInvalidCredentials();

    public override string Message { get; }
    public override int HttpCode { get; }
        

    public BasicError(string name, int value) : base(name, value)
    {
    }

    private sealed class ErrorUnknown : ErrorCode
    {
        public override string Message => "Unknown error occured.";
        public override int HttpCode => (int)HttpStatusCode.BadRequest;

        public ErrorUnknown(): base(nameof(ERR_UNKNOWN), 1001)
        {
            
        }
    }

    private sealed class ErrorValidation : ErrorCode
    {
        public override string Message => "One or more model validation errors occured.";
        public override int HttpCode => (int)HttpStatusCode.UnprocessableEntity;

        public ErrorValidation(): base(nameof(ERR_VALIDATION), 1002)
        {
            
        }
    }
        
    private sealed class ErrorInvalidCredentials : ErrorCode
    {
        public override string Message => "There was no account matched with provided credentials.";
        public override int HttpCode => (int)HttpStatusCode.BadRequest;

        public ErrorInvalidCredentials(): base(nameof(ERR_INVALID_CREDENTIALS), 1003)
        {
            
        }
    }
}