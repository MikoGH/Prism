using Microsoft.EntityFrameworkCore;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Database;

public class PrismContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Theme> Themes { get; set; }

    public DbSet<ThemeVersion> ThemeVersions { get; set; }

    public DbSet<ThemeField> ThemeFields { get; set; }

    public DbSet<ThemeFieldVersion> ThemeFieldVersions { get; set; }

    public DbSet<Record> Records { get; set; }

    public DbSet<RecordVersion> RecordVersions { get; set; }

    public DbSet<RecordValue> RecordValues { get; set; }

    public DbSet<RecordValueVersion> RecordValuesVersion { get; set; }

    public DbSet<UserRate> UserRates { get; set; }

    public DbSet<Rate> Rates { get; set; }

    public PrismContext(DbContextOptions<PrismContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrismContext).Assembly);
    }
}
