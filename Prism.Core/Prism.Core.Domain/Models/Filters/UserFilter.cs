using Monq.Core.MvcExtensions.Attributes;
using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.Domain.Models.Filters;

public class UserFilter
{
    [FilteredBy(nameof(User.Id))]
    public List<Guid>? Ids { get; set; }

    [FilteredBy(nameof(User.Role))]
    public List<Role>? Roles { get; set; }

    [FilteredBy(nameof(User.Name))]
    public List<string>? Names { get; set; }
}
