using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/skills")]
public class SkillsController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get() {
        GetAllSkillsQuery query = new();
        List<SkillDTO> skills = await _mediator.Send(query);
        return Ok(skills);
    }
}