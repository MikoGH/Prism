using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Drawing;

namespace Prism.Core.Domain.Converters;

public class ColorToInt32Converter : ValueConverter<Color, int>
{
    public ColorToInt32Converter()
        : base(
            c => c.ToArgb(),
            v => Color.FromArgb(v)
        )
    { }
}
