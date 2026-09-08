namespace Prism.Core.WebApi.Dtos.Theme;

public class AddThemeFullDto
{
    public Guid UserId { get; set; }

    public AddThemeVersionDto ThemeVersion { get; set; }

    public IEnumerable<AddThemeFieldVersionDto> ThemeFieldVersions { get; set; }
}
