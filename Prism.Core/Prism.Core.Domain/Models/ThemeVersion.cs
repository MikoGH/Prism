namespace Prism.Core.Domain.Models;

public class ThemeVersion
{
    public Guid Id { get; set; }

    public Guid ThemeId { get; set; }

    public Guid UserId { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public string Name { get; set; }
}
