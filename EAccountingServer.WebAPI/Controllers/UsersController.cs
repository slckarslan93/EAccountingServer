using EAccountingServer.Application.Features.Users.CreateUser;
using EAccountingServer.Application.Features.Users.DeleteUserById;
using EAccountingServer.Application.Features.Users.GetAllUsers;
using EAccountingServer.Application.Features.Users.UpdateUser;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers;

public sealed class UsersController : ApiController
{

    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllUsersQuery requsest , CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(requsest, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand requsest , CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(requsest, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateUserCommand requsest , CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(requsest, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteUserByIdCommand requsest , CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(requsest, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
       
}
