using Monq.Core.MvcExtensions.Attributes;

namespace Prism.Core.Domain.Models.Filters;

public class ThemeVersionFilter
{
    [FilteredBy(nameof(ThemeVersion.Id))]
    public List<Guid>? Ids { get; set; }

    [FilteredBy(nameof(ThemeVersion.ThemeId))]
    public List<Guid>? ThemeIds { get; set; }

    [FilteredBy(nameof(ThemeVersion.UserCreateId))]
    public List<Guid>? UserCreateIds { get; set; }

    [FilteredBy(nameof(ThemeVersion.IsAccepted))]
    public List<bool>? IsAccepted { get; set; }
}
