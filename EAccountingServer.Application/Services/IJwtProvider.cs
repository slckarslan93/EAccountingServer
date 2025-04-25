using EAccountingServer.Application.Features.Auth.Login;
using EAccountingServer.Domain.Entities;

namespace EAccountingServer.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
