using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevFreela.API.Controllers;

[Route("api/users")]
public class UsersController : ControllerBase {
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator) {
        this._mediator = mediator;
    }

    // api/users/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
        var query = new GetUserQuery(id);
        var user = await _mediator.Send(query);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // api/users
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserInputModel command) {
        var id = await this._mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = id }, command);
    }

    // api/users/1/login
    [HttpPut("{id}/login")]
    public IActionResult Login(int id, [FromBody] LoginModel loginModel) {
        // TODO: Para Módulo de Autenticação e Autorização
        return NoContent();
    }
}