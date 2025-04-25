using AutoMapper;
using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler(UserManager<AppUser> userManager,IMapper mapper): IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExist = await userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);

        if (isUserNameExist)
        {
            return Result<string>.Failure("Bu Kullanıcı Adı Zaten Kullanılıyor");
        }

        bool isEmailExist = await userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken);

        if (isEmailExist)
        {
            return Result<string>.Failure("Bu E-posta Adresi Zaten Kullanılıyor");
        }
        AppUser appUser = mapper.Map<AppUser>(request);

        IdentityResult identityResult = await userManager.CreateAsync(appUser, request.Password);

        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
        }
        
        // Onay maili gonderilecek

        return "Kullanıcı Başarıyla Oluşturuldu";

    }
}