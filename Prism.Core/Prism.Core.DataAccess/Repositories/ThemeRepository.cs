using Microsoft.EntityFrameworkCore;
using Prism.Core.DataAccess.Database;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Repositories;

public class ThemeRepository : IThemeRepository
{
    private readonly PrismContext _context;

    public ThemeRepository(PrismContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Theme>> GetAllAsync(CancellationToken token = default)
    {
        return await _context.Themes.ToListAsync(token);
    }

    public Task<Theme?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _context.Themes.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<Theme?> AddAsync(Theme theme, CancellationToken token = default)
    {
        var addedTheme = await _context.Themes.AddAsync(theme, token);
        await _context.SaveChangesAsync(token);

        return addedTheme.Entity;
    }
}
