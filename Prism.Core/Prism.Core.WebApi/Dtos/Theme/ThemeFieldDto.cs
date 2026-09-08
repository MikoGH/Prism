using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeFieldDto
{
    public Guid Id { get; set; }

    public Guid ThemeId { get; set; }

    public FieldType Type { get; set; }
}
