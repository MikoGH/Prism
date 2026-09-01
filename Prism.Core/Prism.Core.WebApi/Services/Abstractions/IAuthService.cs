using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Services.Abstractions;

public interface IAuthService
{
    public Task<string?> AuthAsync(AuthUser authUser, CancellationToken token);

    public Task<bool> RegisterAsync(RegisterUser registerUser, CancellationToken token);
}
