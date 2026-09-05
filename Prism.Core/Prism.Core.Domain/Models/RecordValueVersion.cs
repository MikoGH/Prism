namespace Prism.Core.Domain.Models;

public class RecordValueVersion
{
    public Guid Id { get; set; }

    public Guid RecordValueId { get; set; }
    public RecordValue? RecordValue { get; set; }

    public Guid UserCreateId { get; set; }
    public User? UserCreate { get; set; }

    public Guid? UserAcceptId { get; set; }
    public User? UserAccept { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public bool IsChecked { get; set; }

    public int? IntValue { get; set; }

    public double? DoubleValue { get; set; }

    public string? StringValue { get; set; }

    public bool? BoolValue { get; set; }

    public DateTime? DateValue { get; set; }
}
