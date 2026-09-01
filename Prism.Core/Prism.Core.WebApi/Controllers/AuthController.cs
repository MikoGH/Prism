using Microsoft.AspNetCore.Mvc;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Dtos.Auth;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Services.Contracts;

namespace Prism.Core.WebApi.Controllers;

[Controller]
[Route($"{AppConstants.RoutePrefix}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthMapper _authMapper;
    private readonly RegisterMapper _registerMapper;
    private readonly IAuthService _authService;

    public AuthController(AuthMapper authMapper, RegisterMapper registerMapper, IAuthService authService)
    {
        _authMapper = authMapper;
        _registerMapper = registerMapper;
        _authService = authService;
    }

    [HttpPost("auth")]
    public async Task<ActionResult<string?>> Auth([FromBody] AuthDto authDto, CancellationToken token)
    {
        var authUser = _authMapper.ToAuthUser(authDto);

        var jwt = await _authService.AuthAsync(authUser, token);

        return Ok(jwt);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken token)
    {
        var registerUser = _registerMapper.ToRegisterUser(registerDto);

        await _authService.RegisterAsync(registerUser, token);

        return Ok();
    }
}
