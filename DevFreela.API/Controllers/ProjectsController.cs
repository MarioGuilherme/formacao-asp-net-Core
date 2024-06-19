using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase {
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService) {
        this._projectService = projectService;
    }

    // api/projects?quety=net core
    [HttpGet]
    public IActionResult Get(string query) {
        var projects = this._projectService.GetAll(query);
        return Ok(projects);
    }

    // api/projects/3
    [HttpGet("{id}")]
    public IActionResult GetById(int id) {
        ProjectDetailsViewModel projectDetailsViewModel = this._projectService.GetById(id);

        if (projectDetailsViewModel == null)
            return NotFound();

        return Ok(projectDetailsViewModel);
    }

    // api/projects/
    [HttpPost]
    public IActionResult Post([FromBody] NewProjectInputModel inputModel) {
        if (inputModel.Title.Length > 50)
            return BadRequest();
        int id = this._projectService.Create(inputModel);
        return CreatedAtAction(nameof(GetById), new { id }, inputModel);
    }

    // api/projects/3
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel) {
        if (inputModel.Description.Length > 200)
            return BadRequest();
        this._projectService.Update(inputModel);
        return NoContent();
    }

    // api/projects/3
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        this._projectService.Delete(id);
        return NoContent();
    }

    // api/projects/1/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel) {
        this._projectService.CreateComment(inputModel);
        return NoContent();
    }

    // api/projects/1/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id) {
        this._projectService.Start(id);
        return NoContent();
    }

    // api/projects/1/finish
    [HttpPut("{id}/finish")]
    public IActionResult Finish(int id) {
        this._projectService.Finish(id);
        return NoContent();
    }
}