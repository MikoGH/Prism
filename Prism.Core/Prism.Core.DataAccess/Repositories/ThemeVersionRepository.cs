using Flequery.Extensions;
using Flequery.Models;
using Microsoft.EntityFrameworkCore;
using Monq.Core.Paging.Extensions;
using Prism.Core.DataAccess.Database;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.DataAccess.Repositories;

public class ThemeVersionRepository : IThemeVersionRepository
{
    private readonly PrismContext _context;

    public ThemeVersionRepository(PrismContext context)
    {
        _context = context;
    }

    public Task<PagedResponse<ThemeVersion>> FilterAsync(QueryRequest request, CancellationToken token = default)
    {
        return _context.ThemeVersions
            .AsNoTracking()
            .ApplyAsync(request, token);
    }

    public Task<PagedResponse<ThemeVersion>> FilterActualAsync(QueryRequest request, CancellationToken token = default)
    {
        return _context.ThemeVersions
            .AsNoTracking()
            .ApplyIncluding(request.Includes)
            .ApplyFiltering(request.Filters)
            .ApplySearching(request.Search)
            .GroupBy(x => x.ThemeId)
            .Select(x => x.OrderByDescending(y => y.WriteDate).First())
            .ApplySorting(request.Sorting)
            .ApplyPagingAsync(request.Paging, token);
    }

    public Task<ThemeVersion?> FirstOrDefaultAsync(Expression<Func<ThemeVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query
            .OrderByDescending(y => y.WriteDate)
            .FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query.FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeVersion?> GetByIdAsync(Guid id, IncludeRequest? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token = default)
    {
        var addedThemeVersion = await _context.ThemeVersions.AddAsync(themeVersion, token);
        await _context.SaveChangesAsync(token);

        return addedThemeVersion.Entity;
    }
}
