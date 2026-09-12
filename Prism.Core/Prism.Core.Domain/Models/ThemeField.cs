using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.Domain.Models;

public class ThemeField
{
    public Guid Id { get; set; }

    public Guid ThemeId { get; set; }
    public Theme? Theme { get; set; }

    public FieldType Type { get; set; }

    public IEnumerable<ThemeFieldVersion>? ThemeFieldVersions { get; set; }
}
