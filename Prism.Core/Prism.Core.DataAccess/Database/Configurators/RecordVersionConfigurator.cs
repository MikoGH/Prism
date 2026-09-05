using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class RecordVersionConfigurator : IEntityTypeConfiguration<RecordVersion>
{
    public void Configure(EntityTypeBuilder<RecordVersion> builder)
    {
        builder.ToTable(DbConstants.TableNames.RecordVersions);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.UserCreateId).HasColumnName(DbConstants.ColumnNames.UserCreateId).IsRequired();
        builder.Property(x => x.UserAcceptId).HasColumnName(DbConstants.ColumnNames.UserAcceptId);
        builder.Property(x => x.WriteDate).HasColumnName(DbConstants.ColumnNames.WriteDate).IsRequired();
        builder.Property(x => x.IsAccepted).HasColumnName(DbConstants.ColumnNames.IsAccepted).IsRequired();
        builder.Property(x => x.IsChecked).HasColumnName(DbConstants.ColumnNames.IsChecked).IsRequired();
        builder.Property(x => x.Name).HasColumnName(DbConstants.ColumnNames.Name).IsRequired();
        builder.Property(x => x.ImageUrl).HasColumnName(DbConstants.ColumnNames.ImageUrl).IsRequired();
        builder.Property(x => x.IsDeleted).HasColumnName(DbConstants.ColumnNames.IsDeleted).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
