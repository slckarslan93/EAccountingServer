using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.DeleteUserById;

internal sealed class DeleteUserByIdCommandHandler(UserManager<AppUser> userManager): IRequestHandler<DeleteUserByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
        {
            return Result<string>.Failure("Kulanıcı bulunamadı");
        }

        user.IsDeleted = true;

        IdentityResult result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Result<string>.Failure(result.Errors.Select(p => p.Description).ToList());
        }

        return "Kullanıcı başarıyla silindi";
    }
}