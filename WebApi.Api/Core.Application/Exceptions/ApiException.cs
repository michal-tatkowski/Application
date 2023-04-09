using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Enums.Errors.Common;

namespace Core.Application.Exceptions;

/// <summary>
/// Główny wyjątek, przechowujący informacje o błędzie biznesowym.
/// </summary>
public sealed class ApiException : Exception
{
    public ErrorCode ErrorCode { get; }
    public List<string> Parameters { get; }
        
    public ApiException(ErrorCode errorCode) : base(errorCode.Message)
    {
        ErrorCode = errorCode;
    }

    public ApiException(ErrorCode errorCode, string message, params string[] parameters) : base(message)
    {
        ErrorCode = errorCode;
        Parameters = parameters.ToList();
    }

    public static ApiException CreateParametrized(ErrorCode errorCode, params string[] parameters)
    {
        string message = errorCode.Message;
        for (int i = Quantity.One; i < parameters.Length; i++)
        {
            message = errorCode.Message.Replace("{" + i + "}", parameters[i]);
        }

        return new ApiException(errorCode, message, parameters);
    }
}