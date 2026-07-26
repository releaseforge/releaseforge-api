using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseForge.Application.Pipelines.Commands.CreatePipeline;
using ReleaseForge.Application.Pipelines.Commands.DeletePipeline;
using ReleaseForge.Application.Pipelines.Commands.UpdatePipeline;
using ReleaseForge.Application.Pipelines.Queries.GetProjectPipelines;

namespace ReleaseForge.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/projects/{projectId:guid}/pipelines")]
public class PipelinesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid projectId)
    {
        var pipelines = await sender.Send(new GetProjectPipelinesQuery(projectId));
        return Ok(pipelines);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid projectId, CreatePipelineCommand command)
    {
        if (projectId != command.ProjectId)
            return BadRequest("ProjectId mismatch");

        var pipeline = await sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { projectId, id = pipeline.Id }, pipeline);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid projectId, Guid id)
    {
        var pipelines = await sender.Send(new GetProjectPipelinesQuery(projectId));
        var pipeline = pipelines.FirstOrDefault(p => p.Id == id);

        if (pipeline is null)
            return NotFound();

        return Ok(pipeline);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid projectId, Guid id, UpdatePipelineCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var pipeline = await sender.Send(command);
        return Ok(pipeline);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid projectId, Guid id)
    {
        await sender.Send(new DeletePipelineCommand(id));
        return NoContent();
    }
}
