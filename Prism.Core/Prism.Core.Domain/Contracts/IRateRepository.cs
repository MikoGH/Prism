using Prism.Core.Domain.Models;

namespace Prism.Core.Domain.Contracts;

public interface IRateRepository
{
    public Task<IEnumerable<Rate>> GetAllAsync(CancellationToken token = default);
}
