using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Database.Configurators;

public class RecordValueConfigurator : IEntityTypeConfiguration<RecordValue>
{
    public void Configure(EntityTypeBuilder<RecordValue> builder)
    {
        builder.ToTable(DbConstants.TableNames.RecordValues);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.RecordId).HasColumnName(DbConstants.ColumnNames.RecordId).IsRequired();
        builder.Property(x => x.ThemeFieldId).HasColumnName(DbConstants.ColumnNames.ThemeFieldId).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
