using Core.Application.Interfaces;
using System;
using NLog;

namespace WebApi.Api.Services;

internal sealed class AppLogger : IAppLogger
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public void Info(string message)
    {
        _logger.Info(message);
    }

    public void Warn(string message)
    {
        _logger.Warn(message);
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Error(Exception exp)
    {
        _logger.Error(exp);
    }
}