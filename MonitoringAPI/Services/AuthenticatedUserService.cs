using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MonitoringAPI.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public int UserId { get; }

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            if(int.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue("uid"), out int userId))
            {
                UserId = userId;
            }
        }
    }
}
