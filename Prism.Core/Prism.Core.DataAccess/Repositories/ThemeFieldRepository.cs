using Flequery.Extensions;
using Flequery.Models;
using Microsoft.EntityFrameworkCore;
using Prism.Core.DataAccess.Database;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.DataAccess.Repositories;

public class ThemeFieldRepository : IThemeFieldRepository
{
    private readonly PrismContext _context;

    public ThemeFieldRepository(PrismContext context)
    {
        _context = context;
    }

    public Task<PagedResponse<ThemeField>> FilterAsync(QueryRequest request, CancellationToken token = default)
    {
        return _context.ThemeFields
            .AsNoTracking()
            .ApplyAsync(request, token);
    }

    public Task<ThemeField?> FirstOrDefaultAsync(Expression<Func<ThemeField, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFields
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query.FirstOrDefaultAsync(predicate, token);
    }

    public Task<ThemeField?> GetByIdAsync(Guid id, IncludeRequest? include = null, CancellationToken token = default)
    {
        var query = _context.ThemeFields
            .AsNoTracking();

        if (include is not null)
            query = query.ApplyIncluding(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<ThemeField?> AddAsync(ThemeField themeField, CancellationToken token = default)
    {
        var addedThemeField = await _context.ThemeFields.AddAsync(themeField, token);
        await _context.SaveChangesAsync(token);

        return addedThemeField.Entity;
    }
}
