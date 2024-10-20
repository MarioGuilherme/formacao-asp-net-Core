using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
[Authorize]
[ApiController]
public class ProjectsController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    // api/projects?query=net core
    [HttpGet]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> Get(GetAllProjectsQuery getAllProjectsQuery) {
        PaginationResult<ProjectViewModel> projects = await this._mediator.Send(getAllProjectsQuery);
        return this.Ok(projects);
    }

    // api/projects/3
    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> GetById(int id) {
        GetProjectByIdQuery query = new(id);
        ProjectDetailsViewModel projectDetailsViewModel = await this._mediator.Send(query);

        if (projectDetailsViewModel is null) return this.NotFound();

        return this.Ok(projectDetailsViewModel);
    }

    // api/projects/
    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command) {
        int id = await this._mediator.Send(command);
        return this.CreatedAtAction(nameof(this.GetById), new { id }, command);
    }

    // api/projects/3
    [HttpPut("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command) {
        await this._mediator.Send(command);
        return this.NoContent();
    }

    // api/projects/3
    [HttpDelete("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Delete(int id) {
        DeleteProjectCommand command = new(id);
        await this._mediator.Send(command);
        return this.NoContent();
    }

    // api/projects/1/comments
    [HttpPost("{id}/comments")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command) {
        await this._mediator.Send(command);
        return this.NoContent();
    }

    // Servindo como repertório
    // POST api/projects/1234/comments
    //[HttpPost("{id}/comments")]
    //public async Task<IActionResult> PostComment(int id, InsertCommentCommand command) {
    //    var result = await _mediator.Send(command);

    //    if (!result.IsSuccess) {
    //        return BadRequest(result.Message);
    //    }

    //    return NoContent();
    //}

    // api/projects/1/start
    [HttpPut("{id}/start")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Start(int id) {
        StartProjectCommand command = new(id);
        await this._mediator.Send(command);
        return this.NoContent();
    }

    // api/projects/1/finish
    [HttpPut("{id}/finish")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command){
        command.Id = id;

        bool result = await this._mediator.Send(command);

        if (!result) return this.BadRequest("O pagamento não pôde ser processado.");

        return this.Accepted();
    }
}