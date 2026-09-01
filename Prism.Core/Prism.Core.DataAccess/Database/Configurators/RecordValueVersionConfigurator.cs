using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class RecordValueVersionConfigurator : IEntityTypeConfiguration<RecordValueVersion>
{
    public void Configure(EntityTypeBuilder<RecordValueVersion> builder)
    {
        builder.ToTable(DbConstants.TableNames.RecordValueVersions);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.RecordValueId).HasColumnName(DbConstants.ColumnNames.RecordValueId).IsRequired();
        builder.Property(x => x.UserId).HasColumnName(DbConstants.ColumnNames.UserId).IsRequired();
        builder.Property(x => x.WriteDate).HasColumnName(DbConstants.ColumnNames.WriteDate).IsRequired();
        builder.Property(x => x.IsAccepted).HasColumnName(DbConstants.ColumnNames.IsAccepted).IsRequired();
        builder.Property(x => x.IntValue).HasColumnName(DbConstants.ColumnNames.IntValue);
        builder.Property(x => x.DoubleValue).HasColumnName(DbConstants.ColumnNames.DoubleValue);
        builder.Property(x => x.StringValue).HasColumnName(DbConstants.ColumnNames.StringValue);
        builder.Property(x => x.BoolValue).HasColumnName(DbConstants.ColumnNames.BoolValue);
        builder.Property(x => x.DateValue).HasColumnName(DbConstants.ColumnNames.DateValue);

        builder.HasKey(x => x.Id);
    }
}
