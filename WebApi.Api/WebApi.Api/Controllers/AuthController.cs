using WebApi.Api.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace WebApi.Api.Controllers;

public sealed class AuthController : BaseApiController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [Produces("application/json")]
    [HttpPost("AuthenticateMentor")]
    public async Task<IActionResult> AuthenticateMentor([FromBody] GetAuthenticationTokenForUserQuery request, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken).ConfigureAwait(false));
    }
}