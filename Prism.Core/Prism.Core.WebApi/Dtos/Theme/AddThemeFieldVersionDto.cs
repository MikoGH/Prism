namespace Prism.Core.WebApi.Dtos.Theme;

public class AddThemeFieldVersionDto
{
    public Guid? PreviousVersionId { get; set; }

    public Guid? ThemeFieldId { get; set; }

    public AddThemeFieldDto? ThemeField { get; set; }

    public int Priority { get; set; }

    public string Name { get; set; }
}
