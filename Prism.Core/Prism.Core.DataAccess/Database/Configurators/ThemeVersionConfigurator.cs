using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class ThemeVersionConfigurator : IEntityTypeConfiguration<ThemeVersion>
{
    public void Configure(EntityTypeBuilder<ThemeVersion> builder)
    {
        builder.ToTable(DbConstants.TableNames.ThemeVersions);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ThemeId).HasColumnName(DbConstants.ColumnNames.ThemeId).IsRequired();
        builder.Property(x => x.UserCreateId).HasColumnName(DbConstants.ColumnNames.UserCreateId).IsRequired();
        builder.Property(x => x.UserAcceptId).HasColumnName(DbConstants.ColumnNames.UserAcceptId);
        builder.Property(x => x.WriteDate).HasColumnName(DbConstants.ColumnNames.WriteDate).IsRequired();
        builder.Property(x => x.IsAccepted).HasColumnName(DbConstants.ColumnNames.IsAccepted).IsRequired();
        builder.Property(x => x.Name)
            .HasColumnName(DbConstants.ColumnNames.Name)
            .HasMaxLength(DbConstants.ColumnSettings.DefaultNameMaxLength)
            .IsRequired();
        builder.Property(x => x.IsDeleted).HasColumnName(DbConstants.ColumnNames.IsDeleted).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
