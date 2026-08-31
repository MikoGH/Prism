namespace Prism.Core.WebApi.Models;

public class UserRate
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid RecordId { get; set; }

    public Guid RateId { get; set; }
}
