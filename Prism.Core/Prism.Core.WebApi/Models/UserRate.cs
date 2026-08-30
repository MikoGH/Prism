namespace Prism.Core.WebApi.Models;

public class UserRate
{
    public Guid Id { get; set; }

    public Guid IdUser { get; set; }

    public Guid IdRecord { get; set; }

    public Guid IdRate { get; set; }
}
