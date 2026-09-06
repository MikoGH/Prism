using Monq.Core.MvcExtensions.Attributes;

namespace Prism.Core.Domain.Models.Filters;

public class ThemeFieldVersionFilter
{
    [FilteredBy(nameof(ThemeFieldVersion.Id))]
    public List<Guid>? Ids { get; set; }

    [FilteredBy(nameof(ThemeFieldVersion.ThemeField.ThemeId))]
    public List<Guid>? ThemeIds { get; set; }

    [FilteredBy(nameof(ThemeFieldVersion.ThemeFieldId))]
    public List<Guid>? ThemeFieldIds { get; set; }

    [FilteredBy(nameof(ThemeFieldVersion.IsAccepted))]
    public List<bool>? IsAccepted { get; set; }
}
