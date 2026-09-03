namespace Prism.Core.Domain.Models;

public class RecordVersion
{
    public Guid Id { get; set; }

    public Guid RecordId { get; set; }
    public Record? Record { get; set; }

    public Guid UserCreateId { get; set; }
    public User? UserCreate { get; set; }

    public Guid? UserAcceptId { get; set; }
    public User? UserAccept { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }
}
