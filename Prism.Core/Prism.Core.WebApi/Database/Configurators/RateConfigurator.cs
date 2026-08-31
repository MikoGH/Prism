using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Converters;
using Prism.Core.WebApi.Models;
using System.Drawing;

namespace Prism.Core.WebApi.Database.Configurators;

public class RateConfigurator : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        builder.ToTable(DbConstants.TableNames.Rates);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Value).HasColumnName(DbConstants.ColumnNames.Value).IsRequired();
        builder.Property(x => x.Letter).HasColumnName(DbConstants.ColumnNames.Letter).IsRequired();
        builder.Property(x => x.Color).HasColumnName(DbConstants.ColumnNames.Color).IsRequired().HasConversion<ColorToInt32Converter>();

        var seedingData = new Rate[] {
            new() { Id = Guid.Parse("34a688e2-472b-41aa-b9b0-fa1331b2c5fe"), Value = 1, Letter = 'F', Color = Color.DarkViolet },
            new() { Id = Guid.Parse("180ae0be-d8dd-415c-b1bb-d1ca81543187"), Value = 2, Letter = 'F', Color = Color.DarkViolet },
            new() { Id = Guid.Parse("f1229048-2cd3-4b74-87a8-90062776a481"), Value = 3, Letter = 'F', Color = Color.DarkViolet },
            new() { Id = Guid.Parse("13524847-c476-41d1-8834-84db11b4a05d"), Value = 4, Letter = 'E', Color = Color.Blue },
            new() { Id = Guid.Parse("5a606102-34ec-4b30-89aa-47e1bdccfea6"), Value = 5, Letter = 'D', Color = Color.Cyan },
            new() { Id = Guid.Parse("3500e2f4-16f0-486f-b75f-147687573ab8"), Value = 6, Letter = 'C', Color = Color.Green },
            new() { Id = Guid.Parse("57c0097b-a044-41d4-a6a7-8b500f7cf08d"), Value = 7, Letter = 'B', Color = Color.Yellow },
            new() { Id = Guid.Parse("a757745b-c04a-4479-aca4-5c64ff8625d4"), Value = 8, Letter = 'A', Color = Color.Orange },
            new() { Id = Guid.Parse("17818146-13fe-4569-b59a-c82828e550bf"), Value = 9, Letter = 'A', Color = Color.Orange },
            new() { Id = Guid.Parse("7d3bdd20-b87c-4344-abe3-30dd224fb0db"), Value = 10, Letter = 'S', Color = Color.Red },
        };
        builder.HasData(seedingData);

        builder.HasKey(x => x.Id);
    }
}
