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

    public async Task<PagedResponse<ThemeVersion>> FilterActualAsync(QueryRequest request, CancellationToken token = default)
    {
        var lastThemeVersionIds = _context.ThemeVersions
            .AsNoTracking()
            .GroupBy(x => x.ThemeId)
            .Select(x => x
                .OrderByDescending(y => y.WriteDate)
                .Select(y => y.Id)
                .First())
            .ToList();

        var themeVersionsQuery = _context.ThemeVersions
            .AsNoTracking()
            .Where(x => lastThemeVersionIds.Contains(x.Id))
            .ApplyFiltering(request.Filters)
            .ApplySearching(request.Search);

        //var themeData = await themeVersionsQuery
        //    .Select(x => new { x.Id, x.ThemeId })
        //    .ToListAsync(token);
        var themeVersionIds = themeVersionsQuery.Select(x => x.Id).ToList();
        var themeIds = themeVersionsQuery.Select(x => x.ThemeId).ToList();

        var lastThemeFieldVersionIds = _context.ThemeFieldVersions
            .AsNoTracking()
            .Include(x => x.ThemeField)
            .Where(x => themeIds.Contains(x.ThemeField!.ThemeId))
            .GroupBy(x => x.ThemeFieldId)
            .Select(x => x
                .OrderByDescending(y => y.WriteDate)
                .Select(y => y.Id)
                .First())
            .ToList();
        var themeFieldVersionsQuery = _context.ThemeFieldVersions
            .AsNoTracking()
            .Where(x => lastThemeFieldVersionIds.Contains(x.Id))
            .ApplyFiltering(request.Filters)
            .ApplySearching(request.Search);

        //var fieldData = await themeFieldVersionsQuery
        //    .Select(x => new { x.Id, x.ThemeFieldId })
        //    .ToListAsync(token);
        var themeFieldVersionIds = themeFieldVersionsQuery.Select(x => x.Id).ToList();
        var themeFieldIds = themeFieldVersionsQuery.Select(x => x.ThemeFieldId).ToList();

        return await _context.ThemeVersions
            .AsNoTracking()
            .Where(x => themeVersionIds.Contains(x.Id))
            .Include(x => x.Theme)
                .ThenInclude(x => x.ThemeFields
                    .Where(themeField => themeFieldIds.Contains(themeField.Id)))
                    .ThenInclude(x => x.ThemeFieldVersions
                        .Where(themeFieldVersion => themeFieldVersionIds.Contains(themeFieldVersion.Id)))
            .ApplySorting(request.Sorting)
            .ApplyPagingAsync(request.Paging, token);
    }

    public Task<PagedResponse<ThemeVersion>> FilterActualAcceptedAsync(QueryRequest request, CancellationToken token = default)
    {
        return _context.ThemeVersions
            .ApplyIncluding(request.Includes)
            .Where(x => x.IsAccepted)
            .GroupBy(x => x.ThemeId)
            .Select(x => x.OrderByDescending(y => y.WriteDate).First())
            .ApplyFiltering(request.Filters)
            .ApplySearching(request.Search)
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
