using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/users")]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    // api/users/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
        GetUserQuery query = new(id);
        UserViewModel user = await this._mediator.Send(query);

        if (user is null) return this.NotFound();

        return this.Ok(user);
    }

    // api/users
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command) {
        int id = await this._mediator.Send(command);
        return this.CreatedAtAction(nameof(this.GetById), new { id }, command);
    }

    // api/users/1/login
    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command) {
        LoginUserViewModel loginUserViewModel = await this._mediator.Send(command);

        if (loginUserViewModel is null) return this.BadRequest();

        return this.Ok(loginUserViewModel);
    }
}