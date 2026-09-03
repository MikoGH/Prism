namespace Prism.Core.WebApi.Dtos.Theme;

public class UpsertThemeFieldVersionDto
{
    public UpsertThemeFieldDto ThemeField { get; set; }

    public int Priority { get; set; }

    public string Name { get; set; }
}
