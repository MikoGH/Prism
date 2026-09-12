namespace Prism.Core.WebApi.Dtos.Calculation;

public class CalculationRequestDto
{
    public Guid UserId { get; set; }

    public List<Guid> FriendIds { get; set; } = [];
}
