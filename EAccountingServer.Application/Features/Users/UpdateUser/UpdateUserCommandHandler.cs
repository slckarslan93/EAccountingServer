using AutoMapper;
using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(UserManager<AppUser> userManager , IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = await userManager.FindByIdAsync(request.Id.ToString());
        bool isMailChanged = false;

        if (appUser is null)
            return Result<string>.Failure("kullanıcı bulunamadı");

        if(appUser.UserName != request.UserName)
        {
            bool isUserNameExist = await userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);
            if (isUserNameExist)
                return Result<string>.Failure("Bu Kullanıcı Adı Zaten Kullanılıyor");
        }
        if (appUser.Email != request.Email)
        {
            bool isEmailExist = await userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken);
            if (isEmailExist)
                return Result<string>.Failure("Bu E-posta Adresi Zaten Kullanılıyor");

            isMailChanged = true;
            appUser.EmailConfirmed = false;
        }
        appUser = mapper.Map(request, appUser);
        IdentityResult identityResult = await userManager.UpdateAsync(appUser);
        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(appUser);

        identityResult = await userManager.ResetPasswordAsync(appUser, token, request.Password);

        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
        }

        if(isMailChanged)
        {
        //Tekrar onay maili gonder
        }

        // Onay maili gonderilecek
        return "Kullanıcı Başarıyla Güncellendi";

    }
}