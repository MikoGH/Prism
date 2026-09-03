namespace Prism.Core.Domain.Models;

public class UserRate
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid RecordId { get; set; }
    public Record? Record { get; set; }

    public Guid RateId { get; set; }
    public Rate? Rate { get; set; }
}
