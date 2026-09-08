using Flequery.Extensions;
using Flequery.Models;
using Microsoft.EntityFrameworkCore;
using Monq.Core.Paging.Extensions;
using Prism.Core.DataAccess.Database;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.DataAccess.Repositories;

public class ThemeFieldVersionRepository : IThemeFieldVersionRepository
{
    private readonly PrismContext _context;

    public ThemeFieldVersionRepository(PrismContext context)
    {
        _context = context;
    }

    public Task<PagedResponse<ThemeFieldVersion>> FilterAsync(QueryRequest request, CancellationToken token = default)
    {
        return _context.ThemeFieldVersions
            .AsNoTracking()
            .ApplyAsync(request, token);
    }

    public Task<PagedResponse<ThemeFieldVersion>> FilterActualAsync(QueryRequest request, CancellationToken token = default)
    {
        return _context.ThemeFieldVersions
            .AsNoTracking()
            .ApplyIncluding(request.Includes)
            .ApplyFiltering(request.Filters)
            .ApplySearching(request.Search)
            .GroupBy(x => x.ThemeFieldId)
            .Select(x => x.OrderByDescending(y => y.WriteDate).First())
            .ApplyPagingAsync(request.Paging, token);
    }

    public Task<ThemeFieldVersion?> FirstOrDefaultAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default)
    {

        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query.FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeFieldVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default)
    {

        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query
            .OrderByDescending(x => x.WriteDate)
            .FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, IncludeRequest? include = null, CancellationToken token = default)
    {

        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, CancellationToken token = default)
    {
        await _context.ThemeFieldVersions.AddRangeAsync(themeFieldVersions, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task<ThemeFieldVersion?> AddAsync(ThemeFieldVersion themeFieldVersion, CancellationToken token = default)
    {
        var addedThemeFieldVersion = await _context.ThemeFieldVersions.AddAsync(themeFieldVersion, token);
        await _context.SaveChangesAsync(token);

        return addedThemeFieldVersion.Entity;
    }
}
