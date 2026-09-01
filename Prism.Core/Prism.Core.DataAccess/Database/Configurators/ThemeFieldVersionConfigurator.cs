using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class ThemeFieldVersionConfigurator : IEntityTypeConfiguration<ThemeFieldVersion>
{
    public void Configure(EntityTypeBuilder<ThemeFieldVersion> builder)
    {
        builder.ToTable(DbConstants.TableNames.ThemeFieldVersions);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ThemeFieldId).HasColumnName(DbConstants.ColumnNames.ThemeFieldId).IsRequired();
        builder.Property(x => x.UserId).HasColumnName(DbConstants.ColumnNames.UserId).IsRequired();
        builder.Property(x => x.WriteDate).HasColumnName(DbConstants.ColumnNames.WriteDate).IsRequired();
        builder.Property(x => x.IsAccepted).HasColumnName(DbConstants.ColumnNames.IsAccepted).IsRequired();
        builder.Property(x => x.Priority).HasColumnName(DbConstants.ColumnNames.Priority).IsRequired();
        builder.Property(x => x.Name).HasColumnName(DbConstants.ColumnNames.Name).IsRequired();
        builder.Property(x => x.IsDeleted).HasColumnName(DbConstants.ColumnNames.IsDeleted).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
