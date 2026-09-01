using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class ThemeFieldConfigurator : IEntityTypeConfiguration<ThemeField>
{
    public void Configure(EntityTypeBuilder<ThemeField> builder)
    {
        builder.ToTable(DbConstants.TableNames.ThemeFields);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ThemeId).HasColumnName(DbConstants.ColumnNames.ThemeId).IsRequired();
        builder.Property(x => x.Type).HasColumnName(DbConstants.ColumnNames.Type).IsRequired();
        builder.Property(x => x.IsDeleted).HasColumnName(DbConstants.ColumnNames.IsDeleted).IsRequired();

        builder.HasKey(x => x.Id);
    }
}

