using Monq.Core.MvcExtensions.Attributes;
using Prism.Core.WebApi.Models.Enums;

namespace Prism.Core.WebApi.Models.Filters;

public class UserFilter
{
    [FilteredBy(nameof(User.Id))]
    public List<Guid>? Ids { get; set; }

    [FilteredBy(nameof(User.Role))]
    public List<Role>? Roles { get; set; }

    [FilteredBy(nameof(User.Name))]
    public List<string>? Names { get; set; }
}
