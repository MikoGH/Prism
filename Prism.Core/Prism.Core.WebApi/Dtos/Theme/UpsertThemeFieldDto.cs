using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.WebApi.Dtos.Theme;

public class UpsertThemeFieldDto
{
    public Guid? Id { get; set; }

    public FieldType Type { get; set; }
}
