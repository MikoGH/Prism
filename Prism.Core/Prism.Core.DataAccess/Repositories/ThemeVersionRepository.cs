using Microsoft.EntityFrameworkCore;
using Monq.Core.MvcExtensions.Extensions;
using Monq.Core.Paging.Extensions;
using Monq.Core.Paging.Models;
using Prism.Core.DataAccess.Database;
using Prism.Core.DataAccess.Extensions;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;
using System.Linq.Expressions;

namespace Prism.Core.DataAccess.Repositories;

public class ThemeVersionRepository : IThemeVersionRepository
{
    private readonly PrismContext _context;

    public ThemeVersionRepository(PrismContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ThemeVersion>> FilterAsync(ThemeVersionFilter filter, PagingModel? paging = null, ThemeVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        query = query.FilterBy(filter);

        if (paging is not null)
            query = query.WithPaging(paging, null, x => x.WriteDate);

        var themeVersions = await query.ToListAsync(token);

        return themeVersions;
    }

    public async Task<IEnumerable<ThemeVersion>> FilterActualAsync(ThemeVersionFilter filter, PagingModel? paging = null, ThemeVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        query = query
            .FilterBy(filter)
            .GroupBy(x => x.ThemeId)
            .Select(x => x.OrderByDescending(y => y.WriteDate).First());

        if (paging is not null)
            query = query.WithPaging(paging, null, x => x.WriteDate);

        var themeVersions = await query.ToListAsync(token);

        return themeVersions;
    }

    public Task<ThemeVersion?> FirstOrDefaultAsync(Expression<Func<ThemeVersion, bool>> predicate, ThemeVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query
            .OrderByDescending(y => y.WriteDate)
            .FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeVersion, bool>> predicate, ThemeVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeVersion?> GetByIdAsync(Guid id, ThemeVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token = default)
    {
        var addedThemeVersion = await _context.ThemeVersions.AddAsync(themeVersion, token);
        await _context.SaveChangesAsync(token);

        return addedThemeVersion.Entity;
    }
}
