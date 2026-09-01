namespace Prism.Core.WebApi.Dtos.Auth;

public class RegisterDto
{
    public string Name { get; set; }

    public string Password { get; set; }

    public string PasswordRepeat { get; set; }
}
