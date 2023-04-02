using Features.Auth.Auth.Commands.ValidateAndLogoutUser;
using Core.Application.cos.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebApi.Api.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace MonitoringAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthenticationService authenticationService;

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await authenticationService.AuthenticateAsync(request));
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] ValidateAndLogoutUserCommand request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(request, cancellationToken));
        }
    }
}
