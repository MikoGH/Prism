namespace Prism.Core.Domain.Models;

public class RecordVersion
{
    public Guid Id { get; set; }

    public Guid RecordId { get; set; }

    public Guid UserId { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }
}
