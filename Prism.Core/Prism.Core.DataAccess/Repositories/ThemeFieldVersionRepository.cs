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

public class ThemeFieldVersionRepository : IThemeFieldVersionRepository
{
    private readonly PrismContext _context;

    public ThemeFieldVersionRepository(PrismContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ThemeFieldVersion>> FilterAsync(ThemeFieldVersionFilter filter, PagingModel? paging = null, ThemeFieldVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        query = query.FilterBy(filter);

        if (paging is not null)
            query = query.WithPaging(paging, null, x => x.WriteDate);

        var themeFieldVersions = await query.ToListAsync(token);

        return themeFieldVersions;
    }

    public async Task<IEnumerable<ThemeFieldVersion>> FilterActualAsync(ThemeFieldVersionFilter filter, PagingModel? paging = null, ThemeFieldVersionInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        query = query
            .FilterBy(filter)
            .GroupBy(x => x.ThemeFieldId)
            .Select(x => x.OrderByDescending(y => y.WriteDate).First());

        if (paging is not null)
            query = query.WithPaging(paging, null, x => x.WriteDate);

        var themeFieldVersions = await query.ToListAsync(token);

        return themeFieldVersions;
    }

    public Task<ThemeFieldVersion?> FirstOrDefaultAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, ThemeFieldVersionInclude? include = null, CancellationToken token = default)
    {

        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeFieldVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, ThemeFieldVersionInclude? include = null, CancellationToken token = default)
    {

        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query
            .OrderByDescending(x => x.WriteDate)
            .FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, ThemeFieldVersionInclude? include = null, CancellationToken token = default)
    {

        var query = _context.ThemeFieldVersions
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<ThemeFieldVersion?> AddAsync(ThemeFieldVersion themeFieldVersion, CancellationToken token = default)
    {
        var addedThemeFieldVersion = await _context.ThemeFieldVersions.AddAsync(themeFieldVersion, token);
        await _context.SaveChangesAsync(token);

        return addedThemeFieldVersion.Entity;
    }
}
