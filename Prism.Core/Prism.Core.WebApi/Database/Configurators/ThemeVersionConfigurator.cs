using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Database.Configurators;

public class ThemeVersionConfigurator : IEntityTypeConfiguration<ThemeVersion>
{
    public void Configure(EntityTypeBuilder<ThemeVersion> builder)
    {
        builder.ToTable(DbConstants.TableNames.ThemeVersions);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ThemeId).HasColumnName(DbConstants.ColumnNames.ThemeId).IsRequired();
        builder.Property(x => x.UserId).HasColumnName(DbConstants.ColumnNames.UserId).IsRequired();
        builder.Property(x => x.WriteDate).HasColumnName(DbConstants.ColumnNames.WriteDate).IsRequired();
        builder.Property(x => x.IsAccepted).HasColumnName(DbConstants.ColumnNames.IsAccepted).IsRequired();
        builder.Property(x => x.Name)
            .HasColumnName(DbConstants.ColumnNames.Name)
            .HasMaxLength(DbConstants.ColumnSettings.DefaultNameMaxLength)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
