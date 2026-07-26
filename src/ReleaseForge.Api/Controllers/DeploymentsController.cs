using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseForge.Application.Deployments.Commands.CancelDeployment;
using ReleaseForge.Application.Deployments.Commands.CreateDeployment;
using ReleaseForge.Application.Deployments.Commands.FailDeployment;
using ReleaseForge.Application.Deployments.Commands.RollbackDeployment;
using ReleaseForge.Application.Deployments.Commands.StartDeployment;
using ReleaseForge.Application.Deployments.Commands.SucceedDeployment;
using ReleaseForge.Application.Deployments.Queries.GetDeploymentById;
using ReleaseForge.Application.Deployments.Queries.GetReleaseDeployments;

namespace ReleaseForge.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/releases/{releaseId:guid}/deployments")]
public class DeploymentsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid releaseId)
    {
        var deployments = await sender.Send(new GetReleaseDeploymentsQuery(releaseId));
        return Ok(deployments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid releaseId, CreateDeploymentCommand command)
    {
        if (releaseId != command.ReleaseId)
            return BadRequest("ReleaseId mismatch");

        var deployment = await sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { releaseId, id = deployment.Id }, deployment);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid releaseId, Guid id)
    {
        var deployment = await sender.Send(new GetDeploymentByIdQuery(id));

        if (deployment is null)
            return NotFound();

        return Ok(deployment);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(Guid releaseId, Guid id)
    {
        var deployment = await sender.Send(new StartDeploymentCommand(id));
        return Ok(deployment);
    }

    [HttpPost("{id:guid}/succeed")]
    public async Task<IActionResult> Succeed(Guid releaseId, Guid id)
    {
        var deployment = await sender.Send(new SucceedDeploymentCommand(id));
        return Ok(deployment);
    }

    [HttpPost("{id:guid}/fail")]
    public async Task<IActionResult> Fail(Guid releaseId, Guid id)
    {
        var deployment = await sender.Send(new FailDeploymentCommand(id));
        return Ok(deployment);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid releaseId, Guid id)
    {
        var deployment = await sender.Send(new CancelDeploymentCommand(id));
        return Ok(deployment);
    }

    [HttpPost("{id:guid}/rollback")]
    public async Task<IActionResult> Rollback(Guid releaseId, Guid id)
    {
        var deployment = await sender.Send(new RollbackDeploymentCommand(id));
        return Ok(deployment);
    }
}
