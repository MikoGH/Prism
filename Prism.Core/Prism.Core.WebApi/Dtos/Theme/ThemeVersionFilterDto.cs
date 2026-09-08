namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeVersionFilterDto
{
    public List<Guid>? Ids { get; set; }

    public List<Guid>? ThemeIds { get; set; }

    public List<Guid>? UserCreateIds { get; set; }

    public List<bool>? IsAccepted { get; set; }
}
