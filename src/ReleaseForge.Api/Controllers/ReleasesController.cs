using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseForge.Application.Releases.Commands.CancelRelease;
using ReleaseForge.Application.Releases.Commands.CreateRelease;
using ReleaseForge.Application.Releases.Commands.DeleteRelease;
using ReleaseForge.Application.Releases.Commands.DeployRelease;
using ReleaseForge.Application.Releases.Commands.PlanRelease;
using ReleaseForge.Application.Releases.Commands.RollbackRelease;
using ReleaseForge.Application.Releases.Commands.StartRelease;
using ReleaseForge.Application.Releases.Commands.UpdateRelease;
using ReleaseForge.Application.Releases.Queries.GetProjectReleases;
using ReleaseForge.Application.Releases.Queries.GetReleaseById;

namespace ReleaseForge.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/projects/{projectId:guid}/releases")]
public class ReleasesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid projectId)
    {
        var releases = await sender.Send(new GetProjectReleasesQuery(projectId));
        return Ok(releases);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid projectId, Guid id)
    {
        var release = await sender.Send(new GetReleaseByIdQuery(id));

        if (release is null)
            return NotFound();

        return Ok(release);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid projectId, CreateReleaseCommand command)
    {
        if (projectId != command.ProjectId)
            return BadRequest("ProjectId mismatch");

        var release = await sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { projectId, id = release.Id }, release);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid projectId, Guid id, UpdateReleaseCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var release = await sender.Send(command);
        return Ok(release);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid projectId, Guid id)
    {
        await sender.Send(new DeleteReleaseCommand(id));
        return NoContent();
    }

    [HttpPost("{id:guid}/plan")]
    public async Task<IActionResult> Plan(Guid projectId, Guid id)
    {
        var release = await sender.Send(new PlanReleaseCommand(id));
        return Ok(release);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(Guid projectId, Guid id)
    {
        var release = await sender.Send(new StartReleaseCommand(id));
        return Ok(release);
    }

    [HttpPost("{id:guid}/deploy")]
    public async Task<IActionResult> Deploy(Guid projectId, Guid id)
    {
        var release = await sender.Send(new DeployReleaseCommand(id));
        return Ok(release);
    }

    [HttpPost("{id:guid}/rollback")]
    public async Task<IActionResult> Rollback(Guid projectId, Guid id)
    {
        var release = await sender.Send(new RollbackReleaseCommand(id));
        return Ok(release);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid projectId, Guid id)
    {
        var release = await sender.Send(new CancelReleaseCommand(id));
        return Ok(release);
    }
}
