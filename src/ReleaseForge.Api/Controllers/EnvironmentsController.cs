using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseForge.Application.Environments.Commands.CreateEnvironment;
using ReleaseForge.Application.Environments.Commands.DeleteEnvironment;
using ReleaseForge.Application.Environments.Commands.UpdateEnvironment;
using ReleaseForge.Application.Environments.Queries.GetProjectEnvironments;

namespace ReleaseForge.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/projects/{projectId:guid}/environments")]
public class EnvironmentsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid projectId)
    {
        var environments = await sender.Send(new GetProjectEnvironmentsQuery(projectId));
        return Ok(environments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid projectId, CreateEnvironmentCommand command)
    {
        if (projectId != command.ProjectId)
            return BadRequest("ProjectId mismatch");

        var env = await sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { projectId, id = env.Id }, env);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid projectId, Guid id)
    {
        var environments = await sender.Send(new GetProjectEnvironmentsQuery(projectId));
        var env = environments.FirstOrDefault(e => e.Id == id);

        if (env is null)
            return NotFound();

        return Ok(env);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid projectId, Guid id, UpdateEnvironmentCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var env = await sender.Send(command);
        return Ok(env);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid projectId, Guid id)
    {
        await sender.Send(new DeleteEnvironmentCommand(id));
        return NoContent();
    }
}
