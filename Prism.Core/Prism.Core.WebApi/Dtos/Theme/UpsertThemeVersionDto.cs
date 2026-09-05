namespace Prism.Core.WebApi.Dtos.Theme;

public class UpsertThemeVersionDto
{
    public Guid? PreviousVersionId { get; set; }

    public UpsertThemeDto Theme { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public IEnumerable<UpsertThemeFieldVersionDto> ThemeFieldVersions { get; set; }
}
