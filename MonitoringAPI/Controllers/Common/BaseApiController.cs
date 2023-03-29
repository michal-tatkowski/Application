using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace WebApi.Api.Controllers.Common
{
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IMediator mediator;

        protected BaseApiController(IMediator mediator) => this.mediator = mediator;
    }
}
