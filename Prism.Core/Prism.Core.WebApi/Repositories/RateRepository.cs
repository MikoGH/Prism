using Microsoft.EntityFrameworkCore;
using Prism.Core.WebApi.Database;
using Prism.Core.WebApi.Models;
using Prism.Core.WebApi.Repositories.Abstractions;

namespace Prism.Core.WebApi.Repositories;

public class RateRepository : IRateRepository
{
    private readonly PrismContext _context;

    public RateRepository(PrismContext context)
    {
        _context = context;
    }

    public IEnumerable<Rate> GetAllRates(CancellationToken token)
    {
        return _context.Rates.AsNoTracking().ToList();
    }
}
