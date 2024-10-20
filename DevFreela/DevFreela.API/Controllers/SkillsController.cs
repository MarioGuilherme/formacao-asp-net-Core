using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/skills")]
[ApiController]
public class SkillsController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get() {
        GetAllSkillsQuery query = new();
        List<SkillViewModel> skills = await this._mediator.Send(query);
        return this.Ok(skills);
    }
}