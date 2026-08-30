namespace Prism.Core.WebApi.Models;

public class ThemeVersion
{
    public Guid Id { get; set; }

    public Guid IdTheme { get; set; }

    public Guid IdUser { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public string Name { get; set; }
}
