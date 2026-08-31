using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Database.Configurators;

public class RecordVersionConfigurator : IEntityTypeConfiguration<RecordVersion>
{
    public void Configure(EntityTypeBuilder<RecordVersion> builder)
    {
        builder.ToTable(DbConstants.TableNames.RecordVersions);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.UserId).HasColumnName(DbConstants.ColumnNames.UserId).IsRequired();
        builder.Property(x => x.WriteDate).HasColumnName(DbConstants.ColumnNames.WriteDate).IsRequired();
        builder.Property(x => x.IsAccepted).HasColumnName(DbConstants.ColumnNames.IsAccepted).IsRequired();
        builder.Property(x => x.Name).HasColumnName(DbConstants.ColumnNames.Name).IsRequired();
        builder.Property(x => x.ImageUrl).HasColumnName(DbConstants.ColumnNames.ImageUrl).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
