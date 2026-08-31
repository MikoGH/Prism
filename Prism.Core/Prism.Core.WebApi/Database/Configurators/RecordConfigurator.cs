using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Database.Configurators;

public class RecordConfigurator : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder.ToTable(DbConstants.TableNames.Records);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ThemeId).HasColumnName(DbConstants.ColumnNames.ThemeId).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
