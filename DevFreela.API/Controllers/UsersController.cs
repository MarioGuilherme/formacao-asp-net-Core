using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/users")]
[Authorize]
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
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command) {
        var id = await this._mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = id }, command);
    }

    // api/users/1/login
    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command) {
        var loginUserViewModel = await this._mediator.Send(command);

        if (loginUserViewModel is null)
            return BadRequest();

        return Ok(loginUserViewModel);
    }
}