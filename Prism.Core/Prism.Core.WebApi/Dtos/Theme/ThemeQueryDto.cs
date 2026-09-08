namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeQueryDto
{
    public ThemeVersionFilterDto ThemeVersionFilter { get; set; } = new ThemeVersionFilterDto();

    public ThemeFieldVersionFilterDto ThemeFieldVersionFilter { get; set; } = new ThemeFieldVersionFilterDto();

    public ThemeVersionIncludeDto ThemeVersionInclude { get; set; } = new ThemeVersionIncludeDto();

    public ThemeFieldVersionIncludeDto ThemeFieldVersionInclude { get; set; } = new ThemeFieldVersionIncludeDto();
}
