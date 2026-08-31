using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Converters;
using Prism.Core.WebApi.Models;

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

        builder.HasKey(x => x.Id);
    }
}
