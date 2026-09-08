using Prism.Core.WebApi.Dtos.User;

namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeFieldVersionDto
{
    public Guid Id { get; set; }

    public Guid ThemeFieldId { get; set; }
    public ThemeFieldDto? ThemeField { get; set; }

    public Guid? PreviousVersionId { get; set; }
    public ThemeFieldVersionDto? PreviousVersion { get; set; }

    public Guid UserCreateId { get; set; }
    public UserDto? UserCreate { get; set; }

    public Guid? UserAcceptId { get; set; }
    public UserDto? UserAccept { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public bool IsChecked { get; set; }

    public int Priority { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
