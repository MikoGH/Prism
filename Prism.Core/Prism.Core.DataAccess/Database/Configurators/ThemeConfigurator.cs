using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class ThemeConfigurator : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> builder)
    {
        builder.ToTable(DbConstants.TableNames.Themes);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();

        builder.HasKey(x => x.Id);
    }
}
