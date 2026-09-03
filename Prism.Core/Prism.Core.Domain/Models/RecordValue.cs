namespace Prism.Core.Domain.Models;

public class RecordValue
{
    public Guid Id { get; set; }

    public Guid RecordId { get; set; }
    public Record? Record { get; set; }

    public Guid ThemeFieldId { get; set; }
    public ThemeField? ThemeField { get; set; }
}
