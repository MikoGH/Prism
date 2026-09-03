namespace Prism.Core.Domain.Models;

public class ThemeVersion
{
    public Guid Id { get; set; }

    public Guid ThemeId { get; set; }
    public Theme? Theme { get; set; }

    public Guid UserCreateId { get; set; }
    public User? UserCreate { get; set; }

    public Guid? UserAcceptId { get; set; }
    public User? UserAccept { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
