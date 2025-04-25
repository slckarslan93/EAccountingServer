using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQuery(): IRequest<Result<List<AppUser>>>;

