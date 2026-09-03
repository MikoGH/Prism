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

public class ThemeFieldRepository : IThemeFieldRepository
{
    private readonly PrismContext _context;

    public ThemeFieldRepository(PrismContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ThemeField>> FilterAsync(ThemeFieldFilter filter, PagingModel? paging = null, ThemeFieldInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFields
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        query = query.FilterBy(filter);

        if (paging is not null)
            query = query.WithPaging(paging, null, x => x.ThemeId);

        var themeFields = await query.ToListAsync(token);

        return themeFields;
    }

    public Task<ThemeField?> FirstOrDefaultAsync(Expression<Func<ThemeField, bool>> predicate, ThemeFieldInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFields
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(predicate, token);
    }


    public Task<ThemeField?> GetByIdAsync(Guid id, ThemeFieldInclude? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFields
            .AsNoTracking();

        if (include is not null)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<ThemeField?> AddAsync(ThemeField themeField, CancellationToken token = default)
    {
        var addedThemeField = await _context.ThemeFields.AddAsync(themeField, token);
        await _context.SaveChangesAsync(token);

        return addedThemeField.Entity;
    }
}
