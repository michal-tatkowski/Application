using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace WebApi.Api.Controllers.Common;

[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator mediator;
    
    protected BaseApiController(IMediator mediator)
    {
        this.mediator = mediator;
    }
}