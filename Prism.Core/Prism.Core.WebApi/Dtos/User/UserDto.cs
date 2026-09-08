using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.WebApi.Dtos.User;

public class UserDto
{
    public string Name { get; set; }

    public Role Role { get; set; }
}
