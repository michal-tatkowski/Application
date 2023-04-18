using Core.Application.cos.Authentication;

namespace Core.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    }
}