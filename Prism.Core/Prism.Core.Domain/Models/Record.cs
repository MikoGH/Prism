namespace Prism.Core.Domain.Models;

public class Record
{
    public Guid Id { get; set; }

    public Guid ThemeId { get; set; }
    public Theme? Theme { get; set; }
}
