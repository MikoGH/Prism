using Prism.Core.WebApi.Models.Enums;

namespace Prism.Core.WebApi.Models;

public class ThemeField
{
    public Guid Id { get; set; }

    public Guid IdTheme { get; set; }

    public FieldType Type { get; set; }

    public bool IsDeleted { get; set; }
}
