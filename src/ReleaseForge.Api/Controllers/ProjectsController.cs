using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseForge.Application.Projects.Commands.CreateProject;
using ReleaseForge.Application.Projects.Commands.DeleteProject;
using ReleaseForge.Application.Projects.Commands.UpdateProject;
using ReleaseForge.Application.Projects.Queries.GetProjectById;
using ReleaseForge.Application.Projects.Queries.GetProjects;

namespace ReleaseForge.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProjectsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await sender.Send(new GetProjectsQuery());
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await sender.Send(new GetProjectByIdQuery(id));

        if (project is null)
            return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectCommand command)
    {
        var project = await sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var project = await sender.Send(command);
        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await sender.Send(new DeleteProjectCommand(id));
        return NoContent();
    }
}
