namespace Core.Application.Interfaces;

public interface IPasswordHasher
{
    string Hash(string inputString);
}