using Microsoft.EntityFrameworkCore;
using Prism.Core.DataAccess.Database;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;

namespace Prism.Core.DataAccess.Repositories;

public class RateRepository : IRateRepository
{
    private readonly PrismContext _context;

    public RateRepository(PrismContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rate>> GetAllAsync(CancellationToken token)
    {
        var rates = await _context.Rates.AsNoTracking().ToListAsync(token);

        return rates;
    }
}
