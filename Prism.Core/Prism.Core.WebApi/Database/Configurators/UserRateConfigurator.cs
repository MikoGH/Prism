using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Database.Configurators;

public class UserRateConfigurator : IEntityTypeConfiguration<UserRate>
{
    public void Configure(EntityTypeBuilder<UserRate> builder)
    {
        builder.ToTable(DbConstants.TableNames.UserRates);

        builder.Property(x => x.Id).HasColumnName(DbConstants.ColumnNames.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.UserId).HasColumnName(DbConstants.ColumnNames.UserId).IsRequired();
        builder.Property(x => x.RecordId).HasColumnName(DbConstants.ColumnNames.RecordId).IsRequired();
        builder.Property(x => x.RateId).HasColumnName(DbConstants.ColumnNames.RateId).IsRequired();

        builder.HasKey(x => x.Id);
    }
}

