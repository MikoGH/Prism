using System.ComponentModel.DataAnnotations;

namespace Prism.Core.Domain.Models;

public class ThemeFieldVersion
{
    public Guid Id { get; set; }

    public Guid ThemeFieldId { get; set; }
    public ThemeField? ThemeField { get; set; }

    public Guid UserCreateId { get; set; }
    public User? UserCreate { get; set; }

    public Guid? UserAcceptId { get; set; }
    public User? UserAccept { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public bool IsChecked { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Priority value should be 0 or higher")]
    public int Priority { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}