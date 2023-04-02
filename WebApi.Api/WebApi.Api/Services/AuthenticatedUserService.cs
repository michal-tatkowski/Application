using Core.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebApi.Api.Services;

internal sealed class AuthenticatedUserService : IAuthenticatedUserService
{
    public int UserId { get; }

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        if (int.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue("uid"), out int userId))
        {
            UserId = userId;
        }
    }
}