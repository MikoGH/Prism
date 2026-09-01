namespace Prism.Core.Domain.Models;

public class RecordValueVersion
{
    public Guid Id { get; set; }

    public Guid RecordValueId { get; set; }

    public Guid UserId { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public int? IntValue { get; set; }

    public double? DoubleValue { get; set; }

    public string? StringValue { get; set; }

    public bool? BoolValue { get; set; }

    public DateTime? DateValue { get; set; }
}
