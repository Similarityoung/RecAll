using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecAll.Core.List.Api.Application.Commands;
using RecAll.Core.List.Api.Infrastructure.Services;
using RecAll.Infrastructure.Api;

namespace RecAll.Core.List.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ListController {
    private readonly ILogger<ListController> _logger;
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult<ServiceResultViewModel>> CreateAsync(
        [FromBody] CreateListCommand command) {
        _logger.LogInformation(
            "---Sending command {CommandName} - UserIdentityGuid: {UserIdentityGuid}",
            command.GetType().Name, _identityService.GetUserIdentityGuid(),
            command);
        return (await _mediator.Send(command)).ToServiceResultViewModel();
    }
    
}