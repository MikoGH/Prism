using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Prism.Core.Domain.Models;

public class Rate
{
    public Guid Id { get; set; }

    [Range(1, 10, ErrorMessage = "Rate value should be between 1 and 10")]
    public int Value { get; set; }

    public char Letter { get; set; }

    public Color Color { get; set; }
}
