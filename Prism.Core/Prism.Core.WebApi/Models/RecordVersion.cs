namespace Prism.Core.WebApi.Models;

public class RecordVersion
{
    public Guid Id { get; set; }

    public Guid IdRecord { get; set; }

    public Guid IdUser { get; set; }

    public DateTime WriteDate { get; set; }

    public bool IsAccepted { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }
}
