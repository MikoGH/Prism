namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeDto
{
    public Guid Id { get; set; }

    public IEnumerable<ThemeFieldDto>? ThemeFields { get; set; }
}
