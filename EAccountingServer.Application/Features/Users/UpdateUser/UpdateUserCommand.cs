using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.UpdateUser;
public sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, string UserName, string Email,string Password) : IRequest<Result<string>>;
