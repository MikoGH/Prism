using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.DataAccess.Constants;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database.Configurators;

public class UserConfigurator : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(DbConstants.TableNames.Users);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Role).HasColumnName(DbConstants.ColumnNames.Role).IsRequired();
        builder.Property(x => x.Name).HasColumnName(DbConstants.ColumnNames.Name).IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName(DbConstants.ColumnNames.Password).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
