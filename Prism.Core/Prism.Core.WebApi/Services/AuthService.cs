using Microsoft.IdentityModel.Tokens;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Models;
using Prism.Core.WebApi.Models.Enums;
using Prism.Core.WebApi.Repositories.Abstractions;
using Prism.Core.WebApi.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prism.Core.WebApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string?> AuthAsync(AuthUser authUser, CancellationToken token)
    {
        var user = await _userRepository.FirstOrDefaultAsync(x => x.Name == authUser.Name, token);
        if (user is null)
            throw new AuthException($"User with name {authUser.Name} does not exist.");

        bool isValid = BCrypt.Net.BCrypt.Verify(authUser.Password, user.PasswordHash);
        if (!isValid)
            throw new AuthException("Incorrect password.");

        return GenerateJwtToken(user);
    }

    public async Task<bool> RegisterAsync(RegisterUser registerUser, CancellationToken token)
    {
        if (!CheckPasswordRepeatEquals(registerUser.Password, registerUser.PasswordRepeat, token))
            throw new AuthException("Passwords are not equal.");

        if (await CheckUserExistsAsync(registerUser.Name, token))
            throw new AuthException($"User with name {registerUser.Name} already exists.");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);

        var user = new User
        {
            Name = registerUser.Name,
            PasswordHash = hashedPassword,
            Role = Role.Default
        };

        await _userRepository.AddAsync(user, token);
        return true;
    }

    private bool CheckPasswordRepeatEquals(string password, string passwordRepeat, CancellationToken token)
    {
        if (password != passwordRepeat)
            return false;

        return true;
    }

    private async Task<bool> CheckUserExistsAsync(string name, CancellationToken token)
    {
        var user = await _userRepository.FirstOrDefaultAsync(x => x.Name == name, token);
        if (user is null)
            return true;

        return false;
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSecretKey = _configuration[AppConstants.JwtSecretKeySectionName];
        if (jwtSecretKey is null)
        {
            throw new EmptyConfigurationSectionException("Jwt configuration does not set in appsettings.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
