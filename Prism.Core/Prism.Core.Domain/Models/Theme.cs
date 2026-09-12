namespace Prism.Core.Domain.Models;

public class Theme
{
    public Guid Id { get; set; }

    public IEnumerable<ThemeField>? ThemeFields { get; set; }
}
