using System;

namespace Core.Application.Interfaces;

/// <summary>
/// Serwis odpowiadający za logowanie do konsoli / pliku.
/// </summary>
public interface IAppLogger
{
    void Info(string message);
    void Warn(string message);
    void Debug(string message);
    void Error(string message);
    void Error(Exception ex);
}