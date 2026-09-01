using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.Domain.Models;

public class User
{
    public Guid Id { get; set; }

    public Role Role { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }
}
