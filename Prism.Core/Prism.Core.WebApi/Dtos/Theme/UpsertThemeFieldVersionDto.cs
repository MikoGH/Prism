namespace Prism.Core.WebApi.Dtos.Theme;

public class UpsertThemeFieldVersionDto
{
    public Guid? PreviousVersionId { get; set; }

    public UpsertThemeFieldDto ThemeField { get; set; }

    public int Priority { get; set; }

    public string Name { get; set; }
}
