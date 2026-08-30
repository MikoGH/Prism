using System.ComponentModel.DataAnnotations;

namespace Prism.Core.WebApi.Models;

public class ThemeFieldVersion
{
    public Guid Id { get; set; }

    public Guid IdThemeField { get; set; }

    public Guid IdUser { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Priority value should be 0 or higher")]
    public int Priority { get; set; }

    public string Name { get; set; }
}