using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
[Authorize]
public class ProjectsController : ControllerBase {
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator) {
        this._mediator = mediator;
    }

    // api/projects?query=net core
    [HttpGet]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> Get(string query) {
        var getAllProjectsQuery = new GetAllProjectsQuery(query);
        var projects = await this._mediator.Send(getAllProjectsQuery);
        return Ok(projects);
    }

    // api/projects/3
    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> GetById(int id) {
        var query = new GetProjectByIdQuery(id);
        ProjectDetailsViewModel projectDetailsViewModel = await this._mediator.Send(query);

        if (projectDetailsViewModel == null)
            return NotFound();

        return Ok(projectDetailsViewModel);
    }

    // api/projects/
    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command) {
        int id = await this._mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    // api/projects/3
    [HttpPut("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command) {
        await this._mediator.Send(command);
        return NoContent();
    }

    // api/projects/3
    [HttpDelete("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Delete(int id) {
        var command = new DeleteProjectCommand(id);
        await this._mediator.Send(command);
        return NoContent();
    }

    // api/projects/1/comments
    [HttpPost("{id}/comments")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command) {
        await this._mediator.Send(command);
        return NoContent();
    }

    // api/projects/1/start
    [HttpPut("{id}/start")]
    [Authorize(Roles = "client")]
    public IActionResult Start(int id) {
        var command = new StartProjectCommand(id);
        this._mediator.Send(command);
        return NoContent();
    }

    // api/projects/1/finish
    [HttpPut("{id}/finish")]
    [Authorize(Roles = "client")]
    public IActionResult Finish(int id) {
        var command = new FinishProjectCommand(id);
        this._mediator.Send(command);
        return NoContent();
    }
}