namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeFullDto
{
    public ThemeVersionDto ThemeVersion { get; set; }

    public IEnumerable<ThemeFieldVersionDto> ThemeFieldVersions { get; set; }
}
