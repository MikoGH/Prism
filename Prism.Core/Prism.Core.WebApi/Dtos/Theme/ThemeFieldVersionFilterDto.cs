namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeFieldVersionFilterDto
{
    public List<Guid>? Ids { get; set; }

    public List<Guid>? ThemeIds { get; set; }

    public List<Guid>? ThemeFieldIds { get; set; }

    public List<bool>? IsAccepted { get; set; }
}
