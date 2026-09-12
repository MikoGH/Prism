namespace Prism.Core.WebApi.Models;

public class CalculationRequest
{
    public Guid UserId { get; set; }

    public List<Guid> FriendIds { get; set; } = [];
}
