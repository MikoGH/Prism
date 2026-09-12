using Prism.Core.WebApi.Dtos.User;

namespace Prism.Core.WebApi.Dtos.Theme;

public class ThemeVersionDto
{
    public Guid Id { get; set; }

    public Guid ThemeId { get; set; }
    public ThemeDto? Theme { get; set; }

    public Guid? PreviousVersionId { get; set; }
    public ThemeVersionDto? PreviousVersion { get; set; }

    public Guid UserCreateId { get; set; }
    public UserDto? UserCreate { get; set; }

    public Guid? UserAcceptId { get; set; }
    public UserDto? UserAccept { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public bool IsChecked { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
