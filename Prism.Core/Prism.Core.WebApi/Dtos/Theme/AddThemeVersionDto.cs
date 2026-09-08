namespace Prism.Core.WebApi.Dtos.Theme;

public class AddThemeVersionDto
{
    public Guid? PreviousVersionId { get; set; }

    public Guid? ThemeId { get; set; }

    public string Name { get; set; }
}
