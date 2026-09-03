using Monq.Core.MvcExtensions.Attributes;
using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.Domain.Models.Filters;

public class ThemeFieldFilter
{
    [FilteredBy(nameof(ThemeField.Id))]
    public List<Guid>? Ids { get; set; }

    [FilteredBy(nameof(ThemeField.ThemeId))]
    public List<Guid>? ThemeIds { get; set; }

    [FilteredBy(nameof(ThemeField.Type))]
    public List<FieldType>? Types { get; set; }
}
